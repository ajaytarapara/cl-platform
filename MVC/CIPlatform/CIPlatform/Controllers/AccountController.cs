using CIPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using CI_Platform.Entities.ViewModel;
using CI_Platform.Repositry.Repository.Interface;

namespace CIPlatform.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        private object _accountRepository;

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ValidateLoginDetails(LoginModel N )
        {
            if (ModelState.IsValid)
            {
                
            }
            return View(N);
        }


        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPassword(forgotpassModel obj)
        {
            if (ModelState.IsValid)
            {

                return RedirectToAction("Login");
            }
            return View(obj);
        }


        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterModel obj)
        {
            if (ModelState.IsValid)
            {

                return RedirectToAction("Login");
            }
            return View(obj);
        }

        public IActionResult NewPassword()
        {
            return View();
        }
        public IActionResult Home()
        {
            return View();
        }
        public IActionResult volunteeringmissionpage()
        {
            return View();
        }

    }
}
