using CIPlatform.Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatform.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Admin_login()
        {

            return View();

        }
        [HttpPost]
        public IActionResult Admin_login(Admin_loginModel admin_Login)
        {
            if (ModelState.IsValid)
            {
                string? adminemail = admin_Login.Email;
                string? adminpassword = admin_Login.Password;
                //Boolean is_valid_admin=

            }
            else
            {
                ModelState.AddModelError("credential", "credential is wrong");
            }
            return View();
        }
        public IActionResult User_crud()
        {
            return View();
        }
        public IActionResult Cms_crud()
        {
            return View();
        }
        public IActionResult Admin_story()
        {
            return View();
        }
        public IActionResult Admin_mission_application()
        {
            return View();
        }
        public IActionResult Admin_mission()
        {
            return View();
        }
        public IActionResult Admin_edit_cms()
        {
            return View();
        }
        public IActionResult Admin_add_cms()
        {
            return View();
        }
    }
}
