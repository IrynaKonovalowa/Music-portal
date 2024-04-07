using Music_portal.Models;
using Microsoft.EntityFrameworkCore;

namespace Music_portal.Repository
{
    public interface IGenreRepository
    {
        Task<List<Genre>> GetGenreList();
        public List<Genre> Genres();
        Task<Genre> GetGenre(int id);
        Task CreateGenre(Genre item);
        public Task Delete(int id);
		void Update(Genre item);
        Task Save();
    }
}
