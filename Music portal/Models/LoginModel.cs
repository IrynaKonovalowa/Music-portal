using System.ComponentModel.DataAnnotations;
using Music_portal.Filters;

namespace Music_portal.Models
{
    public class LoginModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "FildRequired")]
        public string? Login { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "FildRequired")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
