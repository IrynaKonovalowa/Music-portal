using Microsoft.EntityFrameworkCore;
using Music_portal.Models;
using System.Numerics;

namespace Music_portal.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ClassContext _context;

        public GenreRepository(ClassContext context)
        {
            _context = context;
        }

        public async Task<List<Genre>> GetGenreList()
        {
            var genreContext = _context.Genres;
            return await genreContext.ToListAsync();
        }

        public List<Genre> Genres()
        {
            var genreContext = _context.Genres;
            return genreContext.ToList();
        }

        public async Task<Genre> GetGenre(int id)
        {
            return await _context.Genres.FindAsync(id);
        }
        public async Task CreateGenre(Genre g)
        {
            await _context.Genres.AddAsync(g);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}