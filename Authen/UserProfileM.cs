using ASM.DataAccess.Repository;
using ASM.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authen
{
    public  class UserProfileM
    {  
        private readonly IAppServices _action;
        private readonly AuthUser _authuser;
        public UserProfileM( IAppServices action, AuthUser authuser)
        {
            _action = action;
            _authuser = authuser;
        }
        public string AppRole()
        {
            return UserP("UserRole");
        }
        public string UserName()
        {
            return UserP("UserName");
        }
        public string UserEmail()
        {
            return UserP("UserEmail");
        }
        public string UserP(string operate)
        {
            var para = new { Operate = operate, UserID = _authuser.UserID, AppID = _authuser.AppID };
            var result = _action.UserProfile.ValueOfT("", para);
            return result.ToString();
        }
        public UserProfile GetUserProfile()
        {
            var para = new { Operate = "AllProfile", UserID = _authuser.UserID, AppID = _authuser.AppID };
            var result = _action.UserProfile.ObjectOfT("", para);
            return result;
        }
        public async Task<UserProfile> GetUserProfileAsync()
        {
            var para = new { Operate = "AllProfile", UserID = _authuser.UserID, AppID = _authuser.AppID };
            var result = await _action.UserProfile.ObjectOfTAsync("", para);
            return result;
        }
    }
}
