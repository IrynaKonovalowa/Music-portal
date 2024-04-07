using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music_portal.Models;
using Music_portal.Repository;
using System.Diagnostics;

namespace Music_portal.Controllers
{
    public class SingerController : Controller
    {
        
        ISingerRepository repo;

        public SingerController(ISingerRepository r)
        {
            repo = r;
        }

        public async Task<IActionResult> Singers()
        {
            var model = await repo.GetSingerList();
            return View(model);
        }

        public IActionResult CreateSinger()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSinger([Bind("Id,Name")] Singer singer)
        {
            if (ModelState.IsValid)
            {
                await repo.CreateSinger(singer);
                await repo.Save();
                return RedirectToAction(nameof(Singers));
            }
            return View(singer);
        }
        public async Task<IActionResult> EditSinger(int? id)
        {
            if (id == null || await repo.GetSingerList() == null)
            {
                return NotFound();
            }
            var singer = await repo.GetSinger((int)id);
            if (singer == null)
            {
                return NotFound();
            }
            return View(singer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSinger(int id, [Bind("Id,Name,Songs")] Singer singer)
        {
            if (id != singer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    repo.Update(singer);
                    await repo.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await SingerExists(singer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Singers));
            }
            return View(singer);
        }

        private async Task<bool> SingerExists(int id)
        {
            List<Singer> list = await repo.GetSingerList();
            return (list?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> DeleteSinger(int? id)
        {
            if (id == null || await repo.GetSingerList() == null)
            {
                return NotFound();
            }

            var singer = await repo.GetSinger((int)id);
            if (singer == null)
            {
                return NotFound();
            }

            return View(singer);
        }

        [HttpPost, ActionName("DeleteSinger")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await repo.GetSingerList() == null)
            {
                return Problem("Entity set 'ClassContext.Singers'  is null.");
            }
            var singer = await repo.GetSinger(id);
            if (singer!= null)
            {
                await repo.Delete(id);
            }


            await repo.Save();
            return RedirectToAction(nameof(Singers));
        }

    }
}
