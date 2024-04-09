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
        IUserRepository userRepo;
        IWebHostEnvironment _appEnvironment;

        public HomeController(ISongRepository r, IGenreRepository gr, ISingerRepository sr, IUserRepository ur, IWebHostEnvironment appEnvironment)
        {
            repo = r;
            gRepo = gr;
            sRepo = sr;
			userRepo = ur;
			_appEnvironment =appEnvironment;
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
        public async Task<IActionResult> EditSong(int? id)
        {
            if (id == null || await repo.GetSongList() == null)
            {
                return NotFound();
            }
            var song = await repo.GetSong((int)id);
            if (song == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(gRepo.Genres(), "Id", "Name");
            ViewData["SingerId"] = new SelectList(sRepo.Singers(), "Id", "Name");
            return View(song);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(1000000000)]
        public async Task<IActionResult> EditSong(int id, [Bind("Id, Title, Year, PathToFile, GenreId, SingerId")] Song song, IFormFile uploadedFile)
        {
            if (id != song.Id)
            {
                return NotFound();
            }
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
                try
                {
                    repo.Update(song);
                    await repo.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await SongExists(song.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(gRepo.Genres(), "Id", "Name");
            ViewData["SingerId"] = new SelectList(sRepo.Singers(), "Id", "Name");
            return View(song);
        }

        private async Task<bool> SongExists(int id)
        {
            List<Song> list = await repo.GetSongList();
            return (list?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> DeleteSong(int? id)
        {
            if (id == null || await repo.GetSongList() == null)
            {
                return NotFound();
            }

            var song = await repo.GetSong((int)id);
            if (song == null)
            {
                return NotFound();
            }
            return View(song);
        }

        [HttpPost, ActionName("DeleteSong")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await repo.GetSongList() == null)
            {
                return Problem("Entity set 'ClassContext.Songs'  is null.");
            }
            var song = await repo.GetSong(id);
            if (song != null)
            {
                await repo.Delete(id);
            }
            await repo.Save();
            return RedirectToAction(nameof(Index));
        }
		public async Task<IActionResult> NewUsers()
		{
		    var model = await userRepo.GetNewUsersList();
            return View(model);
		}
        public async Task<IActionResult> EditUser(int? id)
        {
            if (id == null || await userRepo.GetUserList() == null)
            {
                return NotFound();
            }
            var user = await userRepo.GetUserById((int)id);
            if (user == null)
            {
                return NotFound();
            }            
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(int id, [Bind("Id,FirstName,LastName,Login,Email,Access")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }
            var us = await userRepo.GetUser(user.Login);
            us.Login = user.Login;
            us.Email = user.Email;
            us.FirstName = user.FirstName;
            us.LastName = user.LastName;
            us.Access = user.Access;
            HttpContext.Session.SetString("Acces", us.Access.ToString());

            if (ModelState.IsValid)
            {
                try
                {
                    userRepo.Update(us);
                    await userRepo.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await UserExists(us.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(NewUsers));
            }
            return View(us);
        }
        private async Task<bool> UserExists(int id)
        {
            List<User> list = await userRepo.GetUserList();
            return (list?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> DeleteUser(int? id)
        {
            if (id == null || await userRepo.GetUserList() == null)
            {
                return NotFound();
            }

            var user = await userRepo.GetUserById((int)id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedUser(int id)
        {
            if (await userRepo.GetUserList() == null)
            {
                return Problem("Entity set 'ClassContext.Users'  is null.");
            }
            var user = await userRepo.GetUserById(id);
            if (user != null)
            {
                await userRepo.Delete(id);
            }
            await userRepo.Save();
            return RedirectToAction(nameof(NewUsers));
        }

    }
}
