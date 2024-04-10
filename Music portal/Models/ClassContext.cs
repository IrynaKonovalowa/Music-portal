using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Music_portal.Models
{
    public class ClassContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Singer> Singers { get; set; }
        public DbSet<Song> Songs { get; set; }


        public ClassContext(DbContextOptions<ClassContext> options)
            : base(options)
        {
            if(Database.EnsureCreated())
            {
                Genre genre = new Genre();
                genre.Name = "Classic";
                Genre genre1 = new Genre();
                genre1.Name = "Rock";
                Singer singer = new Singer();
                singer.Name = "Mozart";
                Singer singer1 = new Singer();
                singer1.Name = "Okean Elzy";
                Singer singer2 = new Singer();
                singer2.Name = "Nirvana";
                Singer singer3 = new Singer();
                singer3.Name = "Bon Jovi";
                Songs?.Add(new Song { Title="Turkish march", Genre = genre, Singer = singer, Year = 1783, PathToFile="/Songs/turkish-march-mozart.mp3"});
                Songs?.Add(new Song { Title="Melody of rain", Genre = genre, Singer = singer, Year = 1767, PathToFile="/Songs/melody-of-rain-mozart.mp3" });
                Songs?.Add(new Song { Title="Без бою", Genre = genre1, Singer = singer1, Year = 2005, PathToFile="/Songs/bez-boyu-okean-elzy.mp3" });
                Songs?.Add(new Song { Title="Відчуваю", Genre = genre1, Singer = singer1, Year = 2005, PathToFile="/Songs/vidchuvau-okean-elzi.mp3" });
                Songs?.Add(new Song { Title="Man who sold the world", Genre = genre1, Singer = singer2, Year = 1970, PathToFile="/Songs/man-who-sold-the-world-nirvana.mp3" });
                Songs?.Add(new Song { Title="Smells like teen spirit", Genre = genre1, Singer = singer2, Year = 1991, PathToFile="/Songs/smells-like-teen-spirit-nirvana.mp3" });
                Songs?.Add(new Song { Title="It's my life", Genre = genre1, Singer = singer3, Year = 2000, PathToFile="/Songs/it's-my-life-bon-jovi.mp3" });
                Songs?.Add(new Song { Title="Never say goodbye", Genre = genre1, Singer = singer3, Year = 1986, PathToFile="/Songs/never-say-goodbye-bon-jovi.mp3" });
                               
                SaveChanges();
            }
        }
    }
}
