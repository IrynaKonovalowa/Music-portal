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
            return await _context.Users.Where(m => m.Login == login).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task CreateUs(User u)
        {
            await _context.Users.AddAsync(u);
        }
		public async Task<List<User>> GetNewUsersList()
		{
			return await _context.Users.Where(m => m.Access == 0 || m.Access == 1).ToListAsync();
		}
        public void Update(User u)
        {
            _context.Entry(u).State = EntityState.Modified;
        }
        public async Task Delete(int id)
        {
            User? u = await _context.Users.FindAsync(id);
            if (u != null)
                _context.Users.Remove(u);
        }

        public async Task Save()
        {
           await _context.SaveChangesAsync();
        }      
    }
}