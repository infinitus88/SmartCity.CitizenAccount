using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Api.Models.Emails
{
    public class SetStarredModel
    {
        public int MailId { get; set; }
        public bool Value { get; set; }
    }
}
