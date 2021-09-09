namespace SmartCity.CitizenAccount.Api.Models.Users
{
    public class UserWithTokenModel
    {
        public string AccessToken { get; set; }
        public UserModel UserData { get; set; }
    }
}
