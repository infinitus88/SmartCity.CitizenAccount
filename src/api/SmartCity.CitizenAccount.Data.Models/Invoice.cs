using System;

namespace SmartCity.CitizenAccount.Data.Models
{
    public class Invoice
    {
        public Invoice()
        {
            CreationDate = DateTime.Now;
        }

        public int Id { get; set; }
        public string ServiceName { get; set; }
        public InvoiceCategory Category { get; set; }
        public string CitizenId { get; set; }
        public virtual Citizen Citizen { get; set; }
        public decimal Amount { get; set; }
        public InvoiceType InvoiceType { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
