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
            Database.EnsureCreated();
        }
    }
}
