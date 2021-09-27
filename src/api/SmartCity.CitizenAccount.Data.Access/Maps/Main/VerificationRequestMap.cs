using Microsoft.EntityFrameworkCore;
using SmartCity.CitizenAccount.Data.Access.Maps.Common;
using SmartCity.CitizenAccount.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Data.Access.Maps.Main
{
    public class VerificationRequestMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<VerificationRequest>()
                .ToTable("VerificationRequests")
                .HasKey(x => x.Id);
        }
    }
}
