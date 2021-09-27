using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Api.Models.Users
{
    public class CreateVerificationRequest
    {
        public int UserId { get; set; }
        public string CitizenId { get; set; }
    }
}
