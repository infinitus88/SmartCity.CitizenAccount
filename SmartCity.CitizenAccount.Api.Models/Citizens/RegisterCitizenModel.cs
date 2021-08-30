using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Api.Models.Citizens
{
    public class RegisterCitizenModel
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
