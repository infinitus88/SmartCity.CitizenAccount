using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmartCity.CitizenAccount.Api.Models.Payment
{
    public class MakePaymentModel
    {
        [Required]
        public int ServiceId { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string CitizenId { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
