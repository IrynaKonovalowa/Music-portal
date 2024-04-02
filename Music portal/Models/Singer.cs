using System.Collections.ObjectModel;

namespace Music_portal.Models
{
    public class Singer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Song>? Songs { get; set;}
    }
}
