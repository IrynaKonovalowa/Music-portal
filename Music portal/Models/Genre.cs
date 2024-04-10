using System.ComponentModel.DataAnnotations;

namespace Music_portal.Models
{
    public class Genre
    {
        public int Id { get; set; }
		[Required(ErrorMessage = "Field must be set!")]
		public string Name { get; set; }
        public virtual ICollection<Song>? Songs { get; set; }
    }
}
