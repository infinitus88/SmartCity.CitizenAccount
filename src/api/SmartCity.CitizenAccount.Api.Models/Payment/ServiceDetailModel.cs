using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.CitizenAccount.Api.Models.Payment
{
    public class ServiceDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public string SuccessRedirectUrl { get; set; }
        public string CancelRedirectUrl { get; set; }
        public string MarkPaidRequestUrl { get; set; }
        public string RegistrationDate { get; set; }
    }
}
