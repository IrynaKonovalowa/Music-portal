using System.ComponentModel.DataAnnotations;

namespace Music_portal.Models
{
    public class Song
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Field must be set!")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Field must be set!")]
        public int Year{ get; set; }
        
        public string? PathToFile { get; set; }

        public int GenreId {  get; set; }

        public int SingerId { get; set; }
        
        public virtual Genre? Genre{ get; set; }
        
        public virtual Singer? Singer { get; set; }

        public virtual User? User { get; set; }
    }
}
