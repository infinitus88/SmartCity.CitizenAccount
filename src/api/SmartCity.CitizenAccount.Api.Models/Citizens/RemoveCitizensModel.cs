using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Api.Models.Citizens
{
    public class RemoveCitizensModel
    {
        public RemoveCitizensModel()
        {
            CitizenIds = new string[] { };
        }
        public string[] CitizenIds { get; set; }
    }
}
