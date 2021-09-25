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
            Role = "user";
        }

        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string About { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public string CitizenId { get; set; }
        public bool IsVerified { get; set; }
    }
}
