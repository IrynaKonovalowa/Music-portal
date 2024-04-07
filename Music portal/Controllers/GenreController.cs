using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> EditGenre(int? id)
        {
            if (id == null || await repo.GetGenreList() == null)
            {
                return NotFound();
            }
            var genre = await repo.GetGenre((int)id);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGenre(int id, [Bind("Id,Name,Songs")] Genre genre)
        {
            if (id != genre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    repo.Update(genre);
                    await repo.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await GenreExists(genre.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Genres));
            }
            return View(genre);
        }

        private async Task<bool> GenreExists(int id)
        {
            List<Genre> list = await repo.GetGenreList();
            return (list?.Any(e => e.Id == id)).GetValueOrDefault();
        }

		public async Task<IActionResult> DeleteGenre(int? id)
		{
			if (id == null || await repo.GetGenreList() == null)
			{
				return NotFound();
			}

			var genre = await repo.GetGenre((int)id);
			if (genre == null)
			{
				return NotFound();
			}

			return View(genre);
		}

		[HttpPost, ActionName("DeleteGenre")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (await repo.GetGenreList() == null)
			{
				return Problem("Entity set 'ClassContext.Genres'  is null.");
			}
			var genre = await repo.GetGenre(id);
			if (genre != null)
			{
				await repo.Delete(id);
			}
			await repo.Save();
			return RedirectToAction(nameof(Genres));
		}

	}
}
