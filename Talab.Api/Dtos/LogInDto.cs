using System.ComponentModel.DataAnnotations;

namespace Talab.Api.Dtos
{
    public class LogInDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]

        public string Password { get; set; }
    }
}
