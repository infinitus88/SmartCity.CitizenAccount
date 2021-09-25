using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SmartCity.CitizenAccount.Data.Models
{
    public enum InvoiceType : byte
    {
        [Description("expense")]
        Expense = 0,

        [Description("gain")]
        Gain = 1
    }
}
