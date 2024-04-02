using Microsoft.AspNetCore.Mvc;
using Music_portal.Models;
using Music_portal.Repository;
using System.Diagnostics;

namespace Music_portal.Controllers
{
    public class HomeController : Controller
    {
        
        IUserRepository repo;

        public HomeController(IUserRepository r)
        {
            repo = r;
        }

        public IActionResult Index()
        {
            return View();
        }        
    }
}
