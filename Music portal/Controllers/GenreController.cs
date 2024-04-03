using Microsoft.AspNetCore.Mvc;
using Music_portal.Models;
using Music_portal.Repository;
using System.Diagnostics;

namespace Music_portal.Controllers
{
    public class GenreController : Controller
    {
        
        IGenreRepository repo;

        public GenreController(IGenreRepository r)
        {
            repo = r;
        }

        public async Task<IActionResult> Genres()
        {
            var model = await repo.GetGenreList();
            return View(model);
        }

        public IActionResult CreateGenre()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGenre([Bind("Id,Name")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                await repo.CreateGenre(genre);
                await repo.Save();
                return RedirectToAction(nameof(Genres));
            }
            return View(genre);
        }
    }
}
