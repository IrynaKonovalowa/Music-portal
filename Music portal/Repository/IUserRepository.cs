using Music_portal.Models;
using Microsoft.EntityFrameworkCore;

namespace Music_portal.Repository
{
    public interface IUserRepository
    {        
        Task<List<User>> GetUserList();
        Task <User> GetUser(string login);
        Task<User> GetUserById(int id);
        Task CreateUs(User item);
		Task<List<User>> GetNewUsersList();
        public Task Delete(int id);
        void Update(User item);
        Task Save();
    }
}
