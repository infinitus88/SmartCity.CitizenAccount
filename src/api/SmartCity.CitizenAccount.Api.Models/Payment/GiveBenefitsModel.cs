using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmartCity.CitizenAccount.Api.Models.Payment
{
    public class GiveBenefitsModel
    {
        public GiveBenefitsModel()
        {
            CitizenIds = new string[] { };
        }

        [Required]
        public string[] CitizenIds { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
