using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Api.Models.Users
{
    public class UpdateVerificationRequestsModel
    {
        public UpdateVerificationRequestsModel()
        {
            RequestIds = new int[] { };
        }
        public int[] RequestIds { get; set; }
        public string Status { get; set; }
    }
}
