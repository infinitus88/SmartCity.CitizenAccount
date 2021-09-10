using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Data.Models
{
    public class Email
    {
        public int Id { get; set; }
        public string Sender { get; set; }
        public string SenderName { get; set; }
        public string To { get; set; }
        public string ToName { get; set; }
        public string SenderImg { get; set; }
        public string ToImg { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsStarred { get; set; }
        public DateTime Time { get; set; }
        public string SenderMailFolder { get; set; }
        public string ToMainFolder { get; set; }
        public bool Unread { get; set; }
    }
}
