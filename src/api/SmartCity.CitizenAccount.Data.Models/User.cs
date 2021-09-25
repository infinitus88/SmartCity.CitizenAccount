﻿using SmartCity.CitizenAccount.Data.Models.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Data.Models
{
    public class User
    {
        public User()
        {
            Status = Status.Active;
            Role = "user";
            PhotoUrl = Images.DefaultAvatar;
            About = "";
        }

        public int Id { get; set; }

        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }

        public string PhotoUrl { get; set; }
        public string About { get; set; }
        public Status Status { get; set; }

        public string CitizenId { get; set; }
        public bool IsVerified { get; set; }

        public bool IsDeleted { get; set; }      
        
        public string Role { get; set; }
    }
}
