using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Api.Models.Payment
{
    public class InvoiceModel
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string Category { get; set; }
        public string CitizenId { get; set; }
        public decimal Amount { get; set; }
        public string InvoiceType { get; set; }
        public string CreationDate { get; set; }
    }
}
