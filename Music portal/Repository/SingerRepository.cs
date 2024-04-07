using Microsoft.EntityFrameworkCore;
using Music_portal.Models;
using System.Numerics;

namespace Music_portal.Repository
{
    public class SingerRepository : ISingerRepository
    {
        private readonly ClassContext _context;

        public SingerRepository(ClassContext context)
        {
            _context = context;
        }
        public async Task<List<Singer>> GetSingerList()
        {
            var singerContext = _context.Singers;
            return await singerContext.ToListAsync();
        }
        public List<Singer> Singers()
        {
            var singerContext = _context.Singers;
            return singerContext.ToList();
        }

        public async Task<Singer> GetSinger(int id)
        {
            return await _context.Singers.FindAsync(id);
        }

        public async Task CreateSinger(Singer g)
        {
            await _context.Singers.AddAsync(g);
        }
        public void Update(Singer s)
        {
            _context.Entry(s).State = EntityState.Modified;
        }
        public async Task Delete(int id)
        {
            Singer? s = await _context.Singers.FindAsync(id);
            if (s != null)
                _context.Singers.Remove(s);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}