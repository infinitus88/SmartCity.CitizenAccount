﻿using System.ComponentModel.DataAnnotations;

namespace SmartCity.CitizenAccount.Api.Models.Users
{
    public class UpdateUserModel
    { 
        [Required]
        public string DisplayName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhotoUrl { get; set; }

        [MaxLength(120)]
        public string About { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public string Status { get; set; }

        public string CitizenId { get; set; }
    }
}
