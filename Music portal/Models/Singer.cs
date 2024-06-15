using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Music_portal.Models
{
    public class Singer
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                ErrorMessageResourceName = "FildRequired")]
        public string Name { get; set; }
        public virtual ICollection<Song>? Songs { get; set;}
    }
}
