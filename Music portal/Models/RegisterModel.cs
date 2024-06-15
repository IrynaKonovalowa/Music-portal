using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Music_portal.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "FildRequired")]
        public string? FirstName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "FildRequired")]
        public string? LastName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "FildRequired")]
        public string? Login { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "FildRequired")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessageResourceType = typeof(Resources.Resource),ErrorMessageResourceName = "IncorrectEmail")]
        [Remote(action: "CheckEmail", controller: "Account", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "EmailAlreadyInUse")]
        public string? Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "FildRequired")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "PasswordsDoNotMatch")]
        [DataType(DataType.Password)]
        public string? PasswordConfirm { get; set; }
    }
}
