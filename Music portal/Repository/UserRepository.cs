using Microsoft.EntityFrameworkCore;
using Music_portal.Models;
using System.Numerics;

namespace Music_portal.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ClassContext _context;

        public UserRepository(ClassContext context)
        {
            _context = context;
        }        
        
        public async Task<List<User>> GetUserList()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task <User> GetUser(string login)
        {
            var usersContext = _context.Users.Where(m => m.Login == login);

            return await usersContext.FirstOrDefaultAsync();
        }

        public async Task CreateUs(User u)
        {
            await _context.Users.AddAsync(u);
        }

        public async Task Save()
        {
           await _context.SaveChangesAsync();
        }       
    }
}