using SmartCity.CitizenAccount.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Api.Models.Users
{
    public class UserModel
    {
        public UserModel()
        {
            UserRole = "user";
        }

        public int UId { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PhotoURL { get; set; }
        public string About { get; set; }
        public string UserRole { get; set; }
    }
}
