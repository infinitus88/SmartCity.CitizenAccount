using System;

namespace SmartCity.CitizenAccount.Data.Models
{
    public class Citizen
    {
        public Citizen()
        {
            RegistrationDate = DateTime.Now;
        }

        public string Id { get; set; }
        public string FullName { get; set; }
        public byte Sex { get; set; }
        public DateTime DateOfBirth { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
