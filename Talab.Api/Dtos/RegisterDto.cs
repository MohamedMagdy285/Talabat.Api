using System.ComponentModel.DataAnnotations;

namespace Talab.Api.Dtos
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression(
         @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
         ErrorMessage = "Password must be at least 8 characters and contain uppercase, lowercase, and a number.")]
        public string Password { get; set; }
    }
}
