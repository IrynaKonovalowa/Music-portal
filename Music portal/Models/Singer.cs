using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Music_portal.Models
{
    public class Singer
    {
        public int Id { get; set; }
		[Required(ErrorMessage = "Field must be set!")]
		public string Name { get; set; }
        public virtual ICollection<Song>? Songs { get; set;}
    }
}
