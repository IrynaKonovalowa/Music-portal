using Microsoft.AspNetCore.Mvc;
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
    }
}
