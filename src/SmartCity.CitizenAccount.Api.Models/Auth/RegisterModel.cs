using System.ComponentModel.DataAnnotations;

namespace SmartCity.CitizenAccount.Api.Models.Auth
{
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string DisplayName { get; set; }
    }
}
