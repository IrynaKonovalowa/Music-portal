using Music_portal.Models;
using Microsoft.EntityFrameworkCore;

namespace Music_portal.Repository
{
    public interface IUserRepository
    {        
        Task<List<User>> GetUserList();
        Task <User> GetUser(string login);
        Task CreateUs(User item);
        Task Save();
    }
}
