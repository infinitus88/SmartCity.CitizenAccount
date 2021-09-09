using System.ComponentModel.DataAnnotations;

namespace SmartCity.CitizenAccount.Api.Models.Users
{
    public class UpdateUserModel
    {
        public UpdateUserModel()
        {
            Role = "user";
        }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhotoUrl { get; set; }

        [MaxLength(120)]
        public string About { get; set; }

        public string Role { get; set; }
    }
}
