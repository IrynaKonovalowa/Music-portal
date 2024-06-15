using System.ComponentModel.DataAnnotations;

namespace Music_portal.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                ErrorMessageResourceName = "FildRequired")]
        public string Name { get; set; }
        public virtual ICollection<Song>? Songs { get; set; }
    }
}
