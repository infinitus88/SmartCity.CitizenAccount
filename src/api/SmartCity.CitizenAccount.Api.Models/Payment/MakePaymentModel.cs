using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Api.Models.Payment
{
    public class MakePaymentModel
    {
        public string CitizenId { get; set; }
        public decimal Amount { get; set; }
    }
}
