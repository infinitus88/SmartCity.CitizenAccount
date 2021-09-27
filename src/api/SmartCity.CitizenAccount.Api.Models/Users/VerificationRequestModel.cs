using SmartCity.CitizenAccount.Api.Models.Citizens;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Api.Models.Users
{
    public class VerificationRequestModel
    {
        public int Id { get; set; }
        public UserModel UserData { get; set; }
        public int UserId { get; set; }
        public CitizenModel CitizenData { get; set; }
        public string CitizenId { get; set; }
        public string Status { get; set; }
    }
}
