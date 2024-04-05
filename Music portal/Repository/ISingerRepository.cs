using Music_portal.Models;
using Microsoft.EntityFrameworkCore;

namespace Music_portal.Repository
{
    public interface ISingerRepository
    {
        Task<List<Singer>> GetSingerList();
        public List<Singer> Singers();
        Task<Singer> GetSinger(int id);
        Task CreateSinger(Singer item);
        Task Save();
    }
}
