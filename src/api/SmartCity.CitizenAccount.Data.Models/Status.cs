using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SmartCity.CitizenAccount.Data.Models
{
    public enum Status : byte
    {
        [Description("active")]
        Active = 1,

        [Description("deactivated")]
        Deactivated = 0
    }
}
