using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Api.Models.Emails
{
    public class MarkUnreadModel
    {
        public int[] EmailIds { get; set; }
        public bool UnreadFlag { get; set; }
    }
}
