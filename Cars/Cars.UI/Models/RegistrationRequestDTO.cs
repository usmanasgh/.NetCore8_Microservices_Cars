using System.ComponentModel.DataAnnotations;

namespace Cars.UI.Models
{
    public class RegistrationRequestDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Role { get; set; }
    }
}
