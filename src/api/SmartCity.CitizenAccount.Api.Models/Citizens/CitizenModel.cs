using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SmartCity.CitizenAccount.Api.Models.Citizens
{
    public class CitizenModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte Sex { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
