using System.ComponentModel.DataAnnotations;

namespace Music_portal.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Field must be set!")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Field must be set!")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
