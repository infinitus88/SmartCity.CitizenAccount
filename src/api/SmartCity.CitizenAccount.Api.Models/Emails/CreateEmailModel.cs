using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Api.Models.Emails
{
    public class CreateEmailModel
    {
        public string EmailAdress { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
