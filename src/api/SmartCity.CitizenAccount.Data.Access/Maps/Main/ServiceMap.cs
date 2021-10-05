using Microsoft.EntityFrameworkCore;
using SmartCity.CitizenAccount.Data.Access.Maps.Common;
using SmartCity.CitizenAccount.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Data.Access.Maps.Main
{
    public class ServiceMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            var entity = builder.Entity<Service>();
            entity.ToTable("Services").HasKey(x => x.Id);
        }
    }
}
