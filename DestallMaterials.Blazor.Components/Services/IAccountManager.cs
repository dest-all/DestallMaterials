using Protocol.Models.Entities.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Web.View.Services
{
    public interface IAccountManager : IDisposable
    {
        bool Authorised { get; }
        IPermissionsModel Permissions { get; }
        string UserName { get; }
        Task<bool> AuthoriseStartedSessionAsync();
        Task<string> AuthoriseNewSessionAsync(string login, string password); 

        DisposableCallback SubscribeForPermissionsChange(Action<IPermissionsModel> callback);

        Task EndCurrentSessionAsync();

        /// <summary>
        /// Changes password for login. Returns error message, if doesn't succeed.
        /// </summary>
        /// <returns></returns>
        Task<string> ChangePasswordAsync(string oldPassword, string newPassword);
    }
}
