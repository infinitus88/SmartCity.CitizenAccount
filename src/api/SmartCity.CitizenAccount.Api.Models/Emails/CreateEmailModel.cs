using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Api.Models.Emails
{
    public class CreateEmailModel
    {
        public string EmailAddress { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
