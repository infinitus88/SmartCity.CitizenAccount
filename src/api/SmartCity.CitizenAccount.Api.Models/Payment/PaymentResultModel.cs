using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Api.Models.Payment
{
    public class PaymentResultModel
    {
        public decimal Amount { get; set; }
        public bool IsSucceed { get; set; }
    }
}
