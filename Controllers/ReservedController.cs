using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MyApp.Controllers
{
    public class ReservedController : Controller
    {
        // GET: /Reserved/Index
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        // GET: /Reserved/Admin
        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View();
        }
        // GET: /Reserved/User
        [Authorize(Roles = "User")]
        public IActionResult User()
        {
            return View();
        }
    }
}