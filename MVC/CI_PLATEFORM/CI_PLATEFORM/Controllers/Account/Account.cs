using Microsoft.AspNetCore.Mvc;

namespace CI_PLATEFORM.Controllers.Account
{
    public class Account : Controller
    {
        public IActionResult login()
        {
            return View();
        }
        public IActionResult resetpassword()
        {
            return View();
        }


    }
}
