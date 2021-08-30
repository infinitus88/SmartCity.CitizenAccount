using System;

namespace SmartCity.CitizenAccount.Data.Models
{
    public class PaymentBill
    {
        public PaymentBill()
        {
            CreationDate = DateTime.Now;
        }

        public int Id { get; set; }
        public string CitizenId { get; set; }
        public virtual Citizen Citizen { get; set; }
        public decimal Amount { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
