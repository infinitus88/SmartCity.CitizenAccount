using System.ComponentModel;

namespace SmartCity.CitizenAccount.Data.Models
{
    public enum Gender : byte
    {
        [Description("male")]
        Male = 0,

        [Description("female")]
        Female = 1,

        [Description("other")]
        Other = 2,
    }
}