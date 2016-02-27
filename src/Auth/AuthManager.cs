using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Security;

namespace Auth
{
    public static class AuthManager
    {
        public static void Login(string userId)
        {                        
            FormsAuthentication.SetAuthCookie(userId, true);
        }

        public async static Task Logoff()
        {
            await Task.Run(() => FormsAuthentication.SignOut());
        }
    }
}
