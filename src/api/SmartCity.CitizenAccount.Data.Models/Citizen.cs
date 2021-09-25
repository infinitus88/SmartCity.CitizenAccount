using System;

namespace SmartCity.CitizenAccount.Data.Models
{
    public class Citizen
    {
        public Citizen()
        {
            PhotoUrl = Constants.Images.DefaultAvatar;
            RegistrationDate = DateTime.Now;
            Balance = 0;
        }

        public string Id { get; set; }
        public string FullName { get; set; }
        public string PhotoUrl { get; set; }
        public Gender Gender { get; set; }
        public decimal Balance { get; set; }
        public DateTime DateOfBirth { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
