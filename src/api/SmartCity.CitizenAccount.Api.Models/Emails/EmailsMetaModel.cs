using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Api.Models.Emails
{
    public class EmailsMetaModel
    {
        public EmailsMetaModel()
        {
            Folder = new Dictionary<string, int>();
        }
        public Dictionary<string, int> Folder { get; set; }
    }
}
