using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmartCity.CitizenAccount.Api.Models.Citizens
{
    public class CreateCitizenModel
    {
        [Required]
        public string FullName { get; set; }

        public string PhotoUrl { get; set; }

        [Required]
        public string Gender { get; set; }
        [Required]
        public string DateOfBirth { get; set; }
    }
}
