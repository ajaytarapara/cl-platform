using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Repository.Repository;
using CIPlatform.Repository.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatform.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminrepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        public AdminController(IAdminRepository adminRepository, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _adminrepository = adminRepository;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }
        public IActionResult Admin_login()
        {

            return View();

        }

        public HttpContext GetHttpContext()
        {
            return HttpContext;
        }

        [HttpPost]
        public IActionResult Admin_login(Admin_loginModel admin_Login)
        {
            if (ModelState.IsValid)
            {
                string? adminemail = admin_Login.Email;
                string? adminpassword = admin_Login.Password;
                Boolean is_valid_admin = _adminrepository.validateadmin(adminemail);
                if (is_valid_admin)
                {
                    Boolean is_valid_cred = _adminrepository.validateadmincred(adminemail, adminpassword);
                    if (is_valid_cred)
                    {
                        HttpContext.Session.SetString("Email", adminemail);

                        return RedirectToAction("User_crud", "Admin");

                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Password does not match");
                    }

                }
                else
                {
                    ModelState.AddModelError("Email", "email not found");
                }
            }
            else
            {
                ModelState.AddModelError("credential", "credential is wrong");
            }
            return View();
        }
        public IActionResult User_crud(Admin_user_crudModel admin_User_Crud)
        {
            string adminSessionEmailId = HttpContext.Session.GetString("Email");

            if (adminSessionEmailId == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            else
            {
                string adminemail = adminSessionEmailId;
                Admin adminobj = _adminrepository.findadmin(adminemail);
                admin_User_Crud.adminname = adminobj.FirstName + " " + adminobj.LastName;
                admin_User_Crud.adminavatar = "./images/user1.png";
            }

            return View(admin_User_Crud);
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
