using Microsoft.EntityFrameworkCore;
using SmartCity.CitizenAccount.Data.Access.Maps.Common;
using SmartCity.CitizenAccount.Data.Models;

namespace SmartCity.CitizenAccount.Data.Access.Maps.Main
{
    public class PaymentBillMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            var entity = builder.Entity<PaymentBill>();
            entity.ToTable("PaymentBills").HasKey(x => x.Id);
            entity.Property(x => x.Amount).HasColumnType("decimal(9,2)");
        }
    }
}
