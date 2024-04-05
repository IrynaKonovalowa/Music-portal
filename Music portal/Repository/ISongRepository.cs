using Music_portal.Models;
using Microsoft.EntityFrameworkCore;

namespace Music_portal.Repository
{
    public interface ISongRepository
    {
        Task<List<Song>> GetSongList();
        Task<Song> GetSong(int id);
        Task CreateSong(Song item);
        Task Save();
    }
}
