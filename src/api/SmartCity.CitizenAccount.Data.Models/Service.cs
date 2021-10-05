using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Data.Models
{
    public class Service
    {
        public Service()
        {
            RegistrationDate = DateTime.Now;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public string SuccessRedirectUrl { get; set; }
        public string CancelRedirectUrl { get; set; }
        public string MarkPaidRequestUrl { get; set; }
        public DateTime RegistrationDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
