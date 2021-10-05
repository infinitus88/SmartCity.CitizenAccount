namespace SmartCity.CitizenAccount.Api.Models.Payment
{
    public class UpdateServiceModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public string SuccessRedirectUrl { get; set; }
        public string CancelRedirectUrl { get; set; }
        public string MarkPaidRequestUrl { get; set; }
    }
}
