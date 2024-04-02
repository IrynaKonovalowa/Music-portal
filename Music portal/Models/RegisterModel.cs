using System.ComponentModel.DataAnnotations;

namespace Music_portal.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Field must be set!")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Field must be set!")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Field must be set!")]
        public string? Login { get; set; }
        [Required(ErrorMessage = "Field must be set!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Field must be set!")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        [DataType(DataType.Password)]
        public string? PasswordConfirm { get; set; }
    }
}
