using System.ComponentModel;

namespace SmartCity.CitizenAccount.Data.Models
{
    public enum InvoiceCategory : byte
    {
        [Description("fitness")]
        Fitness = 0,

        [Description("benefits")]
        Benefits = 1,

        [Description("catering")]
        Catering = 2,

        [Description("education")]
        Education = 3,

        [Description("other")]
        Other = 4
    }
}