using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmartCity.CitizenAccount.Api.Models.Users
{
    public class UpdateUserInfoModel
    {
        [Required]
        public string DisplayName { get; set; }

        [Required]
        public string PhotoUrl { get; set; }
    }
}
