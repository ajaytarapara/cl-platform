using CI_PLATEFORM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CI_PLATEFORM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult login()
        {
            return View();
        }

        public IActionResult registration()
        {
            return View();
        }
        public IActionResult resetpassword()
        {
            return View();
        }
        public IActionResult lostpassword()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}