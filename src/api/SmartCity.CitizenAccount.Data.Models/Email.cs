using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Data.Models
{
    public class Email
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string DisplayName { get; set; }
        public string EmailAddress { get; set; }
        public string PhotoUrl { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsStarred { get; set; }
        public DateTime Time { get; set; }
        public string Folder { get; set; }
        public bool Unread { get; set; }

        public Email()
        {
            IsStarred = false;
            Time = DateTime.Now;
            Unread = true;
        }
    }
}
