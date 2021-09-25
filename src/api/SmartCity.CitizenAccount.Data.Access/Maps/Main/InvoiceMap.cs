using Microsoft.EntityFrameworkCore;
using SmartCity.CitizenAccount.Data.Access.Maps.Common;
using SmartCity.CitizenAccount.Data.Models;

namespace SmartCity.CitizenAccount.Data.Access.Maps.Main
{
    public class InvoiceMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            var entity = builder.Entity<Invoice>();
            entity.ToTable("Invoices").HasKey(x => x.Id);
            entity.Property(x => x.Amount).HasColumnType("decimal(9,2)");
        }
    }
}
