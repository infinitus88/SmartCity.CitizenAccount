using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Api.Models.Payment
{
    public class InvoiceDetailModel
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string ServiceEmail { get; set; }
        public string ServiceImage { get; set; }
        public decimal Amount { get; set; }
        public string CitizenName { get; set; }
        public string CreationDate { get; set; }
    }
}
