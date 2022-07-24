using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Web.View.Services.Extensions
{
    public static class AccountExtensions
    {
        public static bool AuthorisationAttemptFinished(this IAccountManager account) => 
            account.Authorised || !string.IsNullOrEmpty(account.UserName);
        
    }
}
