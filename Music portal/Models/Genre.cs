namespace Music_portal.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Song>? Songs { get; set; }
    }
}
