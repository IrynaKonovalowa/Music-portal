using Microsoft.EntityFrameworkCore;
using Music_portal.Models;
using System.Numerics;

namespace Music_portal.Repository
{
    public class SongRepository : ISongRepository
    {
        private readonly ClassContext _context;

        public SongRepository(ClassContext context)
        {
            _context = context;
        }

        public async Task<List<Song>> GetSongList()
        {
            var songContext = _context.Songs;
            return await songContext.ToListAsync();
        }

        public async Task<Song> GetSong(int id)
        {
            return await _context.Songs.FindAsync(id);
        }
        public async Task CreateSong(Song s)
        {
            await _context.Songs.AddAsync(s);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}