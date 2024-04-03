using Music_portal.Models;
using Microsoft.EntityFrameworkCore;

namespace Music_portal.Repository
{
    public interface IGenreRepository
    {
        Task<List<Genre>> GetGenreList();
        Task<Genre> GetGenre(int id);
        Task CreateGenre(Genre item);
        Task Save();
    }
}
