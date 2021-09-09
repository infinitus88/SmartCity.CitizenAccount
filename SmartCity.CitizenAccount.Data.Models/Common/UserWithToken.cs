using SmartCity.CitizenAccount.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Data.Access.Common
{
    public class UserWithToken
    {
        public string AccessToken { get; set; }
        public User UserData { get; set; }
    }
}
