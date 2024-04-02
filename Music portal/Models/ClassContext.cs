using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Music_portal.Models
{
    public class ClassContext: DbContext
    {
        public DbSet<User> Users { get; set; }
                
        public ClassContext(DbContextOptions<ClassContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
