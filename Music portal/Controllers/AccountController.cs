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
                if (await repo.GetUserList() == null)
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

                HttpContext.Session.SetString("Acces", user.Access.ToString());
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
        public async Task<IActionResult> Register(RegisterModel reg)
        {
            if (ModelState.IsValid)
            {
                if (await repo.GetUser(reg.Login) != null)
                {    
                     if (reg.Login == "admin")
						 ModelState.AddModelError("", "It's not possible to register under the login admin!");
                     else                                
					     ModelState.AddModelError("", "This login is already taken!");
                     return View(reg);
                }                  
               
                else
                {
                    User user = new User();
                    user.FirstName = reg.FirstName;
                    user.LastName = reg.LastName;
                    user.Email = reg.Email;
                    user.Login = reg.Login;

					if (reg.Login == "admin")
                        user.Access = 2;

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
                    await repo.CreateUs(user);
                    await repo.Save();
                    return RedirectToAction("Login");
                }
            }

            return View(reg);
        }

        public async Task<IActionResult> CheckEmail(string email)
        {        
        if(await repo.GetUserList() != null)
            {
                foreach (var user in await repo.GetUserList())
                {
                    if (user.Email == email)
                    {
                        return Json(false);
                    }
                }
            }             
           return Json(true);
       }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
