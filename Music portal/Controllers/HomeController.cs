using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Music_portal.Models;
using Music_portal.Repository;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Music_portal.Controllers
{
    public class HomeController : Controller
    {
        
        ISongRepository repo;
        IGenreRepository gRepo;
        ISingerRepository sRepo;
        IWebHostEnvironment _appEnvironment;

        public HomeController(ISongRepository r, IGenreRepository gr, ISingerRepository sr, IWebHostEnvironment appEnvironment)
        {
            repo = r;
            gRepo = gr;
            sRepo = sr;
            _appEnvironment=appEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var model = await repo.GetSongList();
            return View(model);
        }

        public IActionResult CreateSong()
        {
            ViewData["GenreId"] = new SelectList(gRepo.Genres(), "Id", "Name");
            ViewData["SingerId"] = new SelectList(sRepo.Singers(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(1000000000)]
        public async Task<IActionResult> CreateSong([Bind("Id, Title, Year, PathToFile, GenreId, SingerId, Genre, Singer, User")] Song song, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // Путь к папке Files
                string path = "/Songs/" + uploadedFile.FileName; // имя файла

                // Сохраняем файл в папку в каталоге wwwroot
                // Для получения полного пути к каталогу wwwroot применяется свойство WebRootPath объекта IWebHostEnvironment
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream); // копируем файл в поток
                }
                song.PathToFile = path;
            }
            else
            {
                ModelState.AddModelError("", "Please select a file!");
            }
            song.Genre = await gRepo.GetGenre(song.GenreId);
            song.Singer = await sRepo.GetSinger(song.SingerId);

            if (ModelState.IsValid)
            {
                await repo.CreateSong(song);
                await repo.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(gRepo.Genres(), "Id", "Name");
            ViewData["SingerId"] = new SelectList(sRepo.Singers(), "Id", "Name");
            return View(song);
        }
    }
}
