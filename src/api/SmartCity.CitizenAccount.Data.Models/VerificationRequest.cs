using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Data.Models
{
    public class VerificationRequest
    {
        public VerificationRequest()
        {
            Status = VerificationStatus.Pending;
            CreationTime = DateTime.Now;
        }

        public int Id { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public virtual Citizen Citizen { get; set; }
        public string CitizenId { get; set; }
        public VerificationStatus Status { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
