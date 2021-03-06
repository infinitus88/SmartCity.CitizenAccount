using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmartCity.CitizenAccount.Api.Models.Users
{
    public class UpdateVerificationRequestModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
