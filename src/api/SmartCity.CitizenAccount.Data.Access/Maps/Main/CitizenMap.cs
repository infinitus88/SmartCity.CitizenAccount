using Microsoft.EntityFrameworkCore;
using SmartCity.CitizenAccount.Data.Access.Maps.Common;
using SmartCity.CitizenAccount.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Data.Access.Maps.Main
{
    public class CitizenMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            var entity = builder.Entity<Citizen>();
            entity.ToTable("Citizens").HasKey(x => x.Id);
            entity.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            entity
                .Property(x => x.Balance)
                .HasColumnType("decimal(18,2)");

        }
    }
}
