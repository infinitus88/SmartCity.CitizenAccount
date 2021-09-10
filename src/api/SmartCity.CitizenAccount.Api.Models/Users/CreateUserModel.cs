using System.ComponentModel.DataAnnotations;

namespace SmartCity.CitizenAccount.Api.Models.Users
{
    public class CreateUserModel
    {
        public CreateUserModel()
        {
            Role = "user";
        }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
