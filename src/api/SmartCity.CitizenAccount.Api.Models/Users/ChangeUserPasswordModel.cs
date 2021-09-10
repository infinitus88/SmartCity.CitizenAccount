using System.ComponentModel.DataAnnotations;

namespace SmartCity.CitizenAccount.Api.Models.Users
{
    public class ChangeUserPasswordModel
    {
        [Required]
        public string Password { get; set; }
    }
}
