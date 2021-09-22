using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Api.Models.Emails
{
    public class MoveMailsModel
    {
        public int[] EmailIds { get; set; }
        public string Folder { get; set; }
    }
}
