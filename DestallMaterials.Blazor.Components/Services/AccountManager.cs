using Client.Communication;
using Client.Communication.Actions.StandardActions;
using Client.Communication.OpenActions;
using Common.Actions.ExceptionsHandling;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Protocol.Exceptions;
using Protocol.Models.Entities.Permissions;
using Protocol.Models.Entities.ReferrableEntities.DataHolders.User;
using Protocol.Models.StartSessionRequest;
using Protocol.Models.VerifySessionRequest;

namespace Client.Web.View.Services
{
    public class SessionInfo
    {
        public string SessionKey;
        public IPermissionsModel Permissions;
        public IUserReference UserReference;

        [JsonIgnore]
        public bool IsValid => UserReference != null && SessionKey != null;
        [JsonIgnore]
        public uint UserId => UserReference?.ID ?? 0;
        [JsonIgnore]
        public string LoginName => UserReference?.Representation;

        
        [JsonConstructor]
        public SessionInfo(UserReference userReference, PermissionsModel permissions, string sessionKey)
        {
            Permissions = permissions;
            SessionKey = sessionKey;
            UserReference = userReference;
        }

        public SessionInfo()
        { 
        }

        public SessionInfo(IUserModel user, string sessionKey)
        {
            SessionKey = sessionKey;
            Permissions = user.Permissions;
            UserReference = user.Reference;
        }

    }
    public class AccountManager : IAccountManager
    {
        readonly IAccountActions _accountInteractor;
        readonly ISessionActions _sessionInteractor;
        readonly IUser _userDataInteractor;
        readonly ILocalStore _localStore;
        readonly ILogger _logger;
        readonly IClientConfigurator _clientConfigurator;

        const string _sessionInfoLocalStoreKey = "sessionInfo";
        
        uint CurrentUserId => _sessionInfo?.UserId ?? 0;
        string SessionKey => _sessionInfo?.SessionKey;
        

        SessionInfo _sessionInfo = new();

        public AccountManager(
            IBusinessServerActionInvokersNet invokersNet,
            ILocalStore localStore,
            ILogger logger)
        {
            _accountInteractor = invokersNet.OpenActions.AccountActions;
            _sessionInteractor = invokersNet.OpenActions.SessionActions;
            _userDataInteractor = invokersNet.StandardActions.User;
            _localStore = localStore;
            _logger = logger;

            _clientConfigurator = invokersNet.Configurator;

        }

        readonly List<Action<IPermissionsModel>> _permissionChangeCallbacks = new List<Action<IPermissionsModel>>();

        public bool Authorised => CurrentUserId != 0;

        public IPermissionsModel Permissions => _sessionInfo?.Permissions ?? new PermissionsModel();

        public string UserName => _sessionInfo?.LoginName;

        public async Task<bool> AuthoriseStartedSessionAsync()
        {
            var oldPermissions = Permissions;
            SessionInfo sessionFromStore = null;

            sessionFromStore = await ImportSessionFromStoreAsync();
            if (sessionFromStore == null)
            {
                return false;
            }

            try
            {
                _logger.LogInformation($"Logging in as {sessionFromStore.LoginName}.");
                
                IUserModel verification = await VerifySessionAsync(sessionFromStore.SessionKey, sessionFromStore.UserId);
                
                if (verification == null || verification?.Reference?.Empty != false)
                {
                    return false;
                }
                _logger.LogInformation($"Successfully logged in as {sessionFromStore.LoginName}.");

                _sessionInfo = new(verification, sessionFromStore.SessionKey);

                _clientConfigurator.SetupRequestHeaders(headers =>
                {
                    headers["SessionKey"] = SessionKey;
                });

                if (Permissions?.ComputeHashCode() != oldPermissions?.ComputeHashCode())
                {
                    FireAllCallbacks();
                }

                return true;
            }
            catch (ServerSideException ex) 
            {
                _logger.LogInformation($"Failed logging in as {sessionFromStore.LoginName}.");
                return false; 
            }
        }

        private async Task<IUserModel> VerifySessionAsync(string sessionKey, uint userId)
        {
            return await _sessionInteractor.VerifySession.CallAsync(new VerifySessionRequestModel
            {
                SessionKey = sessionKey,
                SessionUserID = userId
            });
        }

        public async Task StartNewSessionAsync(string login, string password)
        {
            _logger.LogInformation($"Logging in as {login}.");
            var startSessionResponse = await _sessionInteractor.StartSession.CallAsync(new StartSessionRequestModel
            {
                Login = login,
                Password = password
            });

            var sessionKey = startSessionResponse.SessionKey;
            var currentUserId = startSessionResponse.User.ID;

            var oldPermissions = Permissions;

            _clientConfigurator.SetupRequestHeaders(headers =>
            {
                headers["SessionKey"] = sessionKey;
            });

            var user = await Run.RepeatUntilSuccessAsync(() => VerifySessionAsync(sessionKey, currentUserId), 500);

            _logger.LogInformation($"Success logging in as {user.Reference.ID} {user.Reference.Representation}.");

            _sessionInfo = new(user, sessionKey);
            
            await PutSessionDataToStoreAsync();
            if (Permissions?.ComputeHashCode() != oldPermissions?.ComputeHashCode())
            {
                FireAllCallbacks();
            }
        }

        public DisposableCallback SubscribeForPermissionsChange(Action<IPermissionsModel> callback)
        {
            _permissionChangeCallbacks.Add(callback);
            return new DisposableCallback(c => _permissionChangeCallbacks.Remove(callback));
        }


        
        async Task<SessionInfo> ImportSessionFromStoreAsync()
        {
            _logger.LogInformation($"Retrieving session info from store...");
            var sessionInfo = await _localStore.GetAsync<SessionInfo>(_sessionInfoLocalStoreKey);

            if (sessionInfo == null)
            {
                _logger.LogInformation($"No previous session found in store.");
                return null;
            }

            return sessionInfo;
        }

        async Task PutSessionDataToStoreAsync()
        {
            await _localStore.PutAsync(_sessionInfoLocalStoreKey, _sessionInfo);
        }

        void FireAllCallbacks()
        {
            foreach (var callback in _permissionChangeCallbacks)
            {
                callback(Permissions);
            }
        }

        bool _checkPermissions;
        const int _waitBetweenChecksSeconds = 15;
        public void InitializeRepetitivePermissionsCheck()
        {
            _checkPermissions = true;
            Task.Run(async () =>
            {
                while (_checkPermissions)
                {
                    await Task.Delay(_waitBetweenChecksSeconds * 1000);
                    var user = await _userDataInteractor.Get.CallAsync(CurrentUserId);
                    if (user.Permissions?.ComputeHashCode()
                        != Permissions?.ComputeHashCode())
                    {
                        _sessionInfo = new(user, SessionKey);
                        FireAllCallbacks();
                    }
                }
            });
        }
        public void StopRepetitivePermissionsCheck()
        {
            _checkPermissions = false;
        }

        public void Dispose()
        {
            StopRepetitivePermissionsCheck();
        }

        public Task EndCurrentSessionAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<string> AuthoriseNewSessionAsync(string login, string password)
        {
            try
            {
                await StartNewSessionAsync(login, password);
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public Task<string> ChangePasswordAsync(string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
