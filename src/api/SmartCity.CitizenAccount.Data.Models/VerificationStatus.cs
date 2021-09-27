using System.ComponentModel;

namespace SmartCity.CitizenAccount.Data.Models
{
    public enum VerificationStatus : byte
    {
        [Description("pending")]
        Pending = 0,

        [Description("rejected")]
        Rejected = 1,

        [Description("accepted")]
        Accepted = 2,

        [Description("not-sent")]
        NotSent = 3
    }
}
