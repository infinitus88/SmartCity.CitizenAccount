using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Api.Models.Emails
{
    public class EmailModel
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string EmailAddress { get; set; }
        public string PhotoUrl { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsStarred { get; set; }
        public string Time { get; set; }
        public string Folder { get; set; }
        public bool Unread { get; set; }
    }
}
