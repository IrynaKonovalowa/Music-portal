using Music_portal.Models;
using Music_portal.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Music_portal.Controllers
{
    public class AccountController: Controller
    {
        IUserRepository repo;
        
        public AccountController(IUserRepository r)
        {
            repo = r;
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel logon)
        {
            if (ModelState.IsValid)
            {
                if (repo.GetUserList() == null)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }
                var user = await repo.GetUser(logon.Login);
                if (user == null)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }
                //var user = users.First();
                string? salt = user.Salt;

                //переводим пароль в байт-массив  
                byte[] password = Encoding.Unicode.GetBytes(salt + logon.Password);

                //вычисляем хеш-представление в байтах  
                byte[] byteHash = SHA256.HashData(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                if (user.Password != hash.ToString())
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }
                HttpContext.Session.SetString("Login", user.Login);
                return RedirectToAction("Index", "Home");
            }
            return View(logon);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterModel reg)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.FirstName = reg.FirstName;
                user.LastName = reg.LastName;
                user.Email = reg.Email;
                user.Login = reg.Login;

                byte[] saltbuf = new byte[16];

                RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
                randomNumberGenerator.GetBytes(saltbuf);

                StringBuilder sb = new StringBuilder(16);
                for (int i = 0; i < 16; i++)
                    sb.Append(string.Format("{0:X2}", saltbuf[i]));
                string salt = sb.ToString();

                //переводим пароль в байт-массив  
                byte[] password = Encoding.Unicode.GetBytes(salt + reg.Password);

                //вычисляем хеш-представление в байтах  
                byte[] byteHash = SHA256.HashData(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                user.Password = hash.ToString();
                user.Salt = salt;
                repo.CreateUs(user);
                repo.Save();
                return RedirectToAction("Login");
            }

            return View(reg);
        }

        public IActionResult CheckEmail(string email)
        {
            if (email == "admin@mail.ru" || email == "admin@gmail.com")
                return Json(false);
            return Json(true);
        }
    }
}
