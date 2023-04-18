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
        //===============================================================================================================
        ////User admin part crud
        //================================================================================================================
        public IActionResult User_crud(Admin_user_crudModel Admin_user_crudModel)
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
                Admin_user_crudModel.adminname = adminobj.FirstName + " " + adminobj.LastName;
                Admin_user_crudModel.adminavatar = "./images/user1.png";

            }

            return View(Admin_user_crudModel);
        }
        [HttpPost]
        public IActionResult User_crud(string searchtext)
        {
            Admin_user_crudModel admin_User_Crud = new Admin_user_crudModel();
            admin_User_Crud.Users = _adminrepository.GetUsers(searchtext).ToList();
            return PartialView("_Admin_user_crud_table", admin_User_Crud);
        }

        [HttpPost]
        public IActionResult AddUser_crud(Admin_user_crudModel model)
        {

            if (ModelState.IsValid)
            {
                string email = model.Email;
                User isvalid = _userRepository.findUser(email);
                if (isvalid == null)
                {

                    User user = new User();
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Email = model.Email;
                    user.Password = model.Password;
                    user.EmployeeId = model.EmplyoeeId;
                    user.CreatedAt = DateTime.Now;
                    user.Department = model.Department;
                    user.PhoneNumber = model.PhoneNumber;
                    _adminrepository.AddUserAdmin(user);
                }
                else
                {
                    ModelState.AddModelError("Email", "Email is already user");
                }
            }
            else
            {
                ModelState.AddModelError("parameter", "wronfg");
            }
            return RedirectToAction("User_crud", "Admin");
        }
        [HttpPost]
        public void DeleteUser_crud(long userid)
        {
            _adminrepository.RemoveUserAdmin(userid);
        }
        [HttpPost]
        public IActionResult editUser_crud(long UserId, string Firstname, string LastName, string Password, string Department, string EmployeeId, long PhoneNumber, string Email)
        {
            if (ModelState.IsValid)
            {
                string email = Email;
                User isvalid = _userRepository.findUser(email);
                if (isvalid == null)
                {
                    long userid = UserId;
                    User useredit = _adminrepository.UpdateUserAdminget(userid);
                    useredit.FirstName = Firstname;
                    useredit.LastName = LastName;
                    useredit.Email = Email;
                    useredit.Password = Password;
                    useredit.EmployeeId = EmployeeId;
                    useredit.CreatedAt = DateTime.Now;
                    useredit.Department = Department;
                    useredit.PhoneNumber = PhoneNumber;
                    _adminrepository.UpdateneedUser(useredit);
                }
                else
                {
                    ModelState.AddModelError("Email", "Email is already user");
                }
            }
            else
            {
                ModelState.AddModelError("parameter", "wronfg");
            }
            return RedirectToAction("User_crud", "Admin");
        }


        //=========================================================================================================================
        ////cms admin part crud
        //===========================================================================================================================
        public IActionResult Cms_crud(Admin_cms_crudModel _Cms_CrudModel)
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
                _Cms_CrudModel.adminname = adminobj.FirstName + " " + adminobj.LastName;
                _Cms_CrudModel.adminavatar = "./images/user1.png";
            }
            return View(_Cms_CrudModel);
        }

        [HttpPost]
        public IActionResult Cms_crud(string searchText)
        {
            Admin_cms_crudModel _Cms_ = new Admin_cms_crudModel();
            _Cms_.CmsPage = _adminrepository.GetCmspages(searchText).ToList();
            return PartialView("_Admin_Cms", _Cms_);
        }
        public IActionResult Admin_add_cms()
        {
            Admin_cms_crudModel _Cms_CrudModels = new Admin_cms_crudModel();
            string adminSessionEmailId = HttpContext.Session.GetString("Email");

            if (adminSessionEmailId == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            else
            {
                string adminemail = adminSessionEmailId;
                Admin adminobj = _adminrepository.findadmin(adminemail);
                _Cms_CrudModels.adminname = adminobj.FirstName + " " + adminobj.LastName;
                _Cms_CrudModels.adminavatar = "./images/user1.png";
            }
            return View(_Cms_CrudModels);
        }
        [HttpPost]
        public IActionResult Admin_add_cms(Admin_cms_crudModel cms_CrudModels)
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
                cms_CrudModels.adminname = adminobj.FirstName + " " + adminobj.LastName;
                cms_CrudModels.adminavatar = "./images/user1.png";
                CmsPage CMS = new CmsPage();
                CMS.Title = cms_CrudModels.Title;
                CMS.Description = cms_CrudModels.Description;
                CMS.Slug = cms_CrudModels.Slug;
                CMS.CreatedAt = DateTime.Now;
                _adminrepository.AddCmsAdmin(CMS);
                return RedirectToAction("Admin_add_cms", "Admin");
            }

        }
        public IActionResult Admin_edit_cms(int CmsId)
        {
            Admin_cms_crudModel admin_Cms_=new Admin_cms_crudModel();
            string adminSessionEmailId = HttpContext.Session.GetString("Email");

            if (adminSessionEmailId == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            string adminemail = adminSessionEmailId;
            Admin adminobj = _adminrepository.findadmin(adminemail);
            admin_Cms_.adminname = adminobj.FirstName + " " + adminobj.LastName;
            admin_Cms_.adminavatar = "./images/user1.png";
            admin_Cms_.CmsId= CmsId;
            return View(admin_Cms_);
        }

        [HttpPost]
        public IActionResult Admin_edit_cms(long CmsId,Admin_cms_crudModel cms_CrudModels)
        {
            string adminSessionEmailId = HttpContext.Session.GetString("Email");

            if (adminSessionEmailId == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            else
            {
                long cmsId = CmsId;
                string adminemail = adminSessionEmailId;
                Admin adminobj = _adminrepository.findadmin(adminemail);
                cms_CrudModels.adminname = adminobj.FirstName + " " + adminobj.LastName;
                cms_CrudModels.adminavatar = "./images/user1.png";
                CmsPage cms=_adminrepository.GetCmsAdmin(cmsId);
                cms.Title = cms_CrudModels.Title;
                cms.Description = cms_CrudModels.Description;
                cms.Slug = cms_CrudModels.Slug;
                cms.UpdatedAt = DateTime.Now;
                _adminrepository.UpdateCmsAdmin(cms);
                return RedirectToAction("Admin_edit_cms", "Admin");
            }

        }
        [HttpPost]
        public void DeleteCms_Admin(long cmsId)
        {
            _adminrepository.DeleteCmsAdmin(cmsId);
        }
        //=========================================================================================================================
        ////story admin part crud
        //============================================================================================================================
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

    }
}
