using Microsoft.EntityFrameworkCore;
using SmartCity.CitizenAccount.Data.Access.Constants;
using SmartCity.CitizenAccount.Data.Access.Helpers;
using SmartCity.CitizenAccount.Data.Access.Maps.Common;
using SmartCity.CitizenAccount.Data.Models;

namespace SmartCity.CitizenAccount.Data.Access.Maps.Main
{
    public class UserMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            var entity = builder.Entity<User>();
            entity.ToTable("Users")
                .HasKey(x => x.Id);

            entity.HasData(new User
            {
                Id = 1,
                DisplayName = "Rustam Minnikhanov",
                Email = "minnikhanovrustam@gmail.com",
                Password = "123456".WithBCrypt(),
                Role = "admin",
                Status = Status.Active,
                PhotoUrl = Images.DefaultAvatar,
                CitizenId = null,
                IsVerified = false,
                About = "",
                IsDeleted = false
            });
        }
    }
}
