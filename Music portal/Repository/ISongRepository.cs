using Music_portal.Models;
using Microsoft.EntityFrameworkCore;

namespace Music_portal.Repository
{
    public interface ISongRepository
    {
        Task<List<Song>> GetSongList();
        Task<Song> GetSong(int id);
        Task CreateSong(Song item);
        public Task Delete(int id);
        void Update(Song item);
        Task Save();
    }
}
