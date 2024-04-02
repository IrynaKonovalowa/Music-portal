namespace Music_portal.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int Year{ get; set; }

        public string PathToFile { get; set; }

        public virtual Genre? Genre{ get; set; }

        public virtual Singer? Singer { get; set; }

        public virtual User? User { get; set; }
    }
}
