using Microsoft.EntityFrameworkCore;
using SmartCity.CitizenAccount.Data.Access.Constants;
using SmartCity.CitizenAccount.Data.Access.Maps.Common;
using SmartCity.CitizenAccount.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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

            entity.Property(x => x.Balance)
                .HasColumnType("decimal(18,2)");

            entity.HasData(new Citizen
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                FullName = "Rustam Minnikhanov",
                Balance = 0,
                Gender = Gender.Male,
                PhotoUrl = Images.DefaultAvatar,
                DateOfBirth = DateTime.ParseExact("02/09/2001", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                IsDeleted = false,
                RegistrationDate = DateTime.Now
            });
        }
    }
}
