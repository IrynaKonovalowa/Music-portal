using System.ComponentModel.DataAnnotations;

namespace Music_portal.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public int Access { get; set; }
        public string? Salt { get; set; }

    }
}
