
using AspNetCoreHero.ToastNotification.Abstractions;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Repository.Repository;
using CIPlatform.Repository.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatform.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminrepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly INotyfService _notyf;
        public AdminController(IAdminRepository adminRepository, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository, IWebHostEnvironment webHostEnvironment, INotyfService notyf)
        {
            _adminrepository = adminRepository;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
            _userRepository = userRepository;
            _notyf = notyf;
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
                        HttpContext.Session.SetString("useremail", adminemail);

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
            string adminSessionEmailId = HttpContext.Session.GetString("useremail");

            if (adminSessionEmailId == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            else
            {
                string adminemail = adminSessionEmailId;
                User adminobj = _adminrepository.findadmin(adminemail);
                Admin_user_crudModel.adminname = adminobj.FirstName + " " + adminobj.LastName;
                Admin_user_crudModel.adminavatar =adminobj.Avatar;

            }

            return View(Admin_user_crudModel);
        }
        [HttpPost]
        public IActionResult User_crud(string searchtext, int pageNumber, int pageSize)
        {
            AdminPageList<User> users = _adminrepository.GetUsers(searchtext, pageNumber, pageSize);
            return PartialView("_Admin_user_crud_table", users);
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
                    user.Avatar = "/images/user1.png";
                    user.Status =Boolean.Parse(model.Status);
                    _adminrepository.AddUserAdmin(user);
                    _notyf.Success("user added successfully", 3);
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
            _notyf.Success("user deleted successfully", 3);
        }
        public IActionResult editUser_crud(long UserId)
        {

            long userid = UserId;
            User useredit = _adminrepository.UpdateUserAdminget(userid);
            Admin_user_crudModel modal=new Admin_user_crudModel();
            modal.FirstName = useredit.FirstName;
            modal.LastName = useredit.LastName;
            modal.Email = useredit.Email;
            modal.PhoneNumber= useredit.PhoneNumber;
            modal.EmplyoeeId = useredit.EmployeeId;
            modal.Department = useredit.Department;
            modal.Password=useredit.Password;
  
            return PartialView("_Edit_user_admin", modal);
          

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
                    useredit.UpdatedAt = DateTime.Now;
                    useredit.Department = Department;
                    useredit.PhoneNumber = PhoneNumber;
                    _notyf.Success("user edited successfully", 3);
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
        public IActionResult Admin_Edit_User(long UserId)
        {

            string adminSessionEmailId = HttpContext.Session.GetString("useremail");
            Admin_user_crudModel modal = new Admin_user_crudModel();
            if (adminSessionEmailId == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            else
            {
                string adminemail = adminSessionEmailId;
                User adminobj = _adminrepository.findadmin(adminemail);
                modal.adminname = adminobj.FirstName + " " + adminobj.LastName;
                modal.adminavatar = adminobj.Avatar;
                User useredit = _adminrepository.UpdateUserAdminget(UserId);
                modal.FirstName = useredit.FirstName;
                modal.LastName = useredit.LastName;
                modal.Email = useredit.Email;
                modal.PhoneNumber = useredit.PhoneNumber;
                modal.EmplyoeeId = useredit.EmployeeId;
                modal.Department = useredit.Department;
                modal.Password = useredit.Password;
                modal.profiletext = useredit.ProfileText;
                modal.whyivol = useredit.WhyIVolunteer;
                modal.UserId = useredit.UserId;
                modal.Status = useredit.Status.ToString();
                return View(modal);

            }

        }
        [HttpPost]
        public IActionResult Admin_Edit_User(Admin_user_crudModel modal)
        {
            long UserId=(long)modal.UserId;
            User useredit = _adminrepository.UpdateUserAdminget(UserId);
            useredit.FirstName=modal.FirstName;
            useredit.LastName=modal.LastName;
            useredit.ProfileText = modal.profiletext;
            useredit.Password = modal.Password;
            useredit.PhoneNumber= modal.PhoneNumber;
            useredit.WhyIVolunteer = modal.whyivol;
            useredit.UpdatedAt = DateTime.Now;
            useredit.Status = Boolean.Parse(modal.Status);
            _adminrepository.UpdateneedUser(useredit);
            _notyf.Success("user edited successfully", 3);
            return RedirectToAction("User_crud", "Admin");

        }
        //=========================================================================================================================
        ////cms admin part crud
        //===========================================================================================================================
        public IActionResult Cms_crud(Admin_cms_crudModel _Cms_CrudModel)
        {
            string adminSessionEmailId = HttpContext.Session.GetString("useremail");

            if (adminSessionEmailId == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            else
            {
                string adminemail = adminSessionEmailId;
                User adminobj = _adminrepository.findadmin(adminemail);
                _Cms_CrudModel.adminname = adminobj.FirstName + " " + adminobj.LastName;
                _Cms_CrudModel.adminavatar = adminobj.Avatar;
            }
            return View(_Cms_CrudModel);
        }

        [HttpPost]
        public IActionResult Cms_crud(string searchText, int pageNumber, int pageSize)
        {
            AdminPageList<CmsPage> _Cms_ = _adminrepository.GetCmspages(searchText, pageNumber, pageSize);
            return PartialView("_Admin_Cms", _Cms_);
        }
        public IActionResult Admin_add_cms()
        {
            Admin_cms_crudModel _Cms_CrudModels = new Admin_cms_crudModel();
            string adminSessionEmailId = HttpContext.Session.GetString("useremail");

            if (adminSessionEmailId == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            else
            {
                string adminemail = adminSessionEmailId;
                User adminobj = _adminrepository.findadmin(adminemail);
                _Cms_CrudModels.adminname = adminobj.FirstName + " " + adminobj.LastName;
                _Cms_CrudModels.adminavatar = adminobj.Avatar;
            }
            return View(_Cms_CrudModels);
        }
        [HttpPost]
        public IActionResult Admin_add_cms(Admin_cms_crudModel cms_CrudModels)
        {
            string adminSessionEmailId = HttpContext.Session.GetString("useremail");

            if (adminSessionEmailId == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            else
            {
                string adminemail = adminSessionEmailId;
                User adminobj = _adminrepository.findadmin(adminemail);
                cms_CrudModels.adminname = adminobj.FirstName + " " + adminobj.LastName;
                cms_CrudModels.adminavatar = adminobj.Avatar;
                CmsPage CMS = new CmsPage();
                CMS.Title = cms_CrudModels.Title;
                CMS.Description = cms_CrudModels.Description;
                CMS.Slug = cms_CrudModels.Slug;
                CMS.CreatedAt = DateTime.Now;
                CMS.Status= cms_CrudModels.Status;
                _adminrepository.AddCmsAdmin(CMS);
                _notyf.Success("cms added successfully", 3);
                return RedirectToAction("Admin_add_cms", "Admin");
            }

        }
        public IActionResult Admin_edit_cms(int CmsId)
        {
            Admin_cms_crudModel admin_Cms_ = new Admin_cms_crudModel();
            string adminSessionEmailId = HttpContext.Session.GetString("useremail");

            if (adminSessionEmailId == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            string adminemail = adminSessionEmailId;
            User adminobj = _adminrepository.findadmin(adminemail);
            admin_Cms_.adminname = adminobj.FirstName + " " + adminobj.LastName;
            admin_Cms_.adminavatar = adminobj.Avatar;
            admin_Cms_.CmsId = CmsId;
            CmsPage cms = _adminrepository.GetCmsAdmin(CmsId);
            admin_Cms_.Title= cms.Title;
            admin_Cms_.Slug = cms.Slug;
            admin_Cms_.Description = cms.Description;
            admin_Cms_.CmsId = (int?)cms.CmsPageId;
            admin_Cms_.Status = cms.Status;
            return View(admin_Cms_);
        }

        [HttpPost]
        public IActionResult Admin_edit_cms(long CmsId, Admin_cms_crudModel cms_CrudModels)
        {
            string adminSessionEmailId = HttpContext.Session.GetString("useremail");

            if (adminSessionEmailId == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            else
            {
                long cmsId = CmsId;
                string adminemail = adminSessionEmailId;
                User adminobj = _adminrepository.findadmin(adminemail);
                cms_CrudModels.adminname = adminobj.FirstName + " " + adminobj.LastName;
                cms_CrudModels.adminavatar = adminobj.Avatar;
                CmsPage cms = _adminrepository.GetCmsAdmin(cmsId);
                cms.Title = cms_CrudModels.Title;
                cms.Description = cms_CrudModels.Description;
                cms.Slug = cms_CrudModels.Slug;
                cms.UpdatedAt = DateTime.Now;
                cms.Status=cms_CrudModels.Status;
                _adminrepository.UpdateCmsAdmin(cms);
                _notyf.Success("cms edited successfully", 3);
                return RedirectToAction("Cms_crud", "Admin");
            }

        }
        [HttpPost]
        public void DeleteCms_Admin(long cmsId)
        {
            _adminrepository.DeleteCmsAdmin(cmsId);
            _notyf.Success("cms deleted successfully", 3);
        }
        //=========================================================================================================================
        ////story admin part crud
        //============================================================================================================================
        public IActionResult Admin_story(Admin_story_crud story_Crud)
        {
            string adminSessionEmailId = HttpContext.Session.GetString("useremail");

            if (adminSessionEmailId == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            string adminemail = adminSessionEmailId;
            User adminobj = _adminrepository.findadmin(adminemail);
            story_Crud.adminname = adminobj.FirstName + " " + adminobj.LastName;
            story_Crud.adminavatar = adminobj.Avatar;
            return View(story_Crud);
        }

        [HttpPost]
        public IActionResult Admin_story(string searchText, int pageNumber, int pageSize)
        {
            AdminPageList<Story> stories = _adminrepository.GetStoryAdmin(searchText, pageNumber, pageSize);
            return PartialView("_Admin_Story_crud", stories);
        }

        [HttpPost]
        public IActionResult ApproveAdmin_story(long storyId)
        {
            Story story = _adminrepository.GetstoryForApprove(storyId);
            story.Status = "approved";
            string adminSessionEmailId = HttpContext.Session.GetString("useremail");

            if (adminSessionEmailId == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            string adminemail = adminSessionEmailId;
            User adminobj = _adminrepository.findadmin(adminemail);
            int fromuserid = (int)adminobj.UserId;
            _adminrepository.ApproveStory(story, fromuserid);
            Admin_story_crud story_Crud = new Admin_story_crud();
            string searchText = "";
            int pageNumber = 1;
            int pageSize = 2;
            AdminPageList<Story> storyLIST = _adminrepository.GetStoryAdmin(searchText, pageNumber, pageSize);
            _notyf.Success("story approved successfully", 3);
            return PartialView("_Admin_Story_crud", storyLIST);

        }
        [HttpPost]
        public IActionResult DeleteAdmin_story(long storyId)
        {
            Story story = _adminrepository.GetstoryForApprove(storyId);
            story.Status = "rejected";
            story.DeletedAt = DateTime.Now;
            string adminSessionEmailId = HttpContext.Session.GetString("useremail");

            if (adminSessionEmailId == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            string adminemail = adminSessionEmailId;
            User adminobj = _adminrepository.findadmin(adminemail);
            int fromuserid = (int)adminobj.UserId;
            _adminrepository.DeleteStory(story, fromuserid);
            string searchText = "";
            int pageNumber = 1;
            int pageSize = 2;
            AdminPageList<Story> storyLIST = _adminrepository.GetStoryAdmin(searchText, pageNumber, pageSize);
            _notyf.Success("story rejected successfully", 3);
            return PartialView("_Admin_Story_crud", storyLIST);
        }
        //=========================================================================================================================
        ////mission application admin part crud
        //============================================================================================================================
        public IActionResult Admin_mission_application(Admin_mission_theme mission_Application)
        {
            string adminSessionEmailId = HttpContext.Session.GetString("useremail");

            if (adminSessionEmailId == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            string adminemail = adminSessionEmailId;
           User adminobj = _adminrepository.findadmin(adminemail);
            mission_Application.adminname = adminobj.FirstName + " " + adminobj.LastName;
            mission_Application.adminavatar = adminobj.Avatar;
            return View(mission_Application);
        }
        [HttpPost]
        public IActionResult Admin_mission_application(string searchText, int pageNumber, int pageSize)
        {
            AdminPageList<MissionApplication> missionapplication = _adminrepository.GetMissionApplicationAdmin(searchText, pageNumber, pageSize);
            return PartialView("_Admin_Mission_Application_crud", missionapplication);
        }
        [HttpPost]
        public IActionResult ApproveAdmin_mission_application(long missionAppId)
        {
            MissionApplication application = _adminrepository.GetApplicationForApprove(missionAppId);
            application.ApprovalStatus = "approved";
            _adminrepository.ApproveApplication(application);
            int pageNumber = 1;
            int pageSize = 2;
            string searchText = "";
            AdminPageList<MissionApplication> missionapplication = _adminrepository.GetMissionApplicationAdmin(searchText, pageNumber, pageSize);
            _notyf.Success("application approved successfully", 3);
            return PartialView("_Admin_Mission_Application_crud", missionapplication);

        }
        [HttpPost]
        public IActionResult DeleteAdmin_mission_application(long missionAppId)
        {
            MissionApplication application = _adminrepository.GetApplicationForApprove(missionAppId);
            application.ApprovalStatus = "rejected";
            _adminrepository.DeleteApplication(application);
            int pageNumber = 1;
            int pageSize = 2;
            string searchText = "";
            AdminPageList<MissionApplication> missionapplication = _adminrepository.GetMissionApplicationAdmin(searchText, pageNumber, pageSize);
            _notyf.Success("application rejected successfully", 3);
            return PartialView("_Admin_Mission_Application_crud", missionapplication);

        }
        //=========================================================================================================================
        ////mission theme admin part crud
        //============================================================================================================================
        public IActionResult Admin_mission_theme(Admin_mission_theme_crudModel theme)
        {
            string adminSessionEmailId = HttpContext.Session.GetString("useremail");

            if (adminSessionEmailId == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            string adminemail = adminSessionEmailId;
            User adminobj = _adminrepository.findadmin(adminemail);
            theme.adminname = adminobj.FirstName + " " + adminobj.LastName;
            theme.adminavatar = adminobj.Avatar;
            return View(theme);
        }
        [HttpPost]
        public IActionResult Admin_mission_theme(string searchText, int pageNumber, int pageSize)
        {
            AdminPageList<MissionTheme> missionthemes = _adminrepository.GetMissionThemeAdmin(searchText, pageNumber, pageSize);
            return PartialView("_Admin_Mission_theme", missionthemes);

        }
        public IActionResult Admin_add_themes()
        {
            Admin_mission_theme_crudModel _Theme_CrudModels = new Admin_mission_theme_crudModel();
            string adminSessionEmailId = HttpContext.Session.GetString("useremail");

            if (adminSessionEmailId == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            else
            {
                string adminemail = adminSessionEmailId;
                User adminobj = _adminrepository.findadmin(adminemail);
                _Theme_CrudModels.adminname = adminobj.FirstName + " " + adminobj.LastName;
                _Theme_CrudModels.adminavatar = adminobj.Avatar;
            }
            return View(_Theme_CrudModels);
        }
        [HttpPost]
        public IActionResult AddAdmin_mission_theme(Admin_mission_theme_crudModel _Theme_CrudModels)
        {
            string adminSessionEmailId = HttpContext.Session.GetString("useremail");

            if (adminSessionEmailId == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            else
            {
                string adminemail = adminSessionEmailId;
               User adminobj = _adminrepository.findadmin(adminemail);
                _Theme_CrudModels.adminname = adminobj.FirstName + " " + adminobj.LastName;
                _Theme_CrudModels.adminavatar = "./images/user1.png";
                MissionTheme theme = new MissionTheme();
                theme.Title = _Theme_CrudModels.themeTitle;
                theme.CreatedAt = DateTime.Now;
                theme.Status =(byte) _Theme_CrudModels.Status;
                _adminrepository.AddThemeAdmin(theme);
                _notyf.Success("theme added successfully", 3);
                return RedirectToAction("Admin_add_themes", "Admin");
            }

        }
        public IActionResult Admin_Edit_themes(long themeId)
        {
            Admin_mission_theme_crudModel _Theme_CrudModels = new Admin_mission_theme_crudModel();
            string adminSessionEmailId = HttpContext.Session.GetString("useremail");

            if (adminSessionEmailId == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            else
            {
                string adminemail = adminSessionEmailId;
                User adminobj = _adminrepository.findadmin(adminemail);
                _Theme_CrudModels.adminname = adminobj.FirstName + " " + adminobj.LastName;
                _Theme_CrudModels.adminavatar = "./images/user1.png";
                _Theme_CrudModels.themeId = themeId;
                MissionTheme themes = _adminrepository.GetThemeAdmin(themeId);
                _Theme_CrudModels.themeTitle = themes.Title;
                _Theme_CrudModels.Status = themes.Status;
            }
            return View(_Theme_CrudModels);

        }
        [HttpPost]
        public IActionResult Admin_Edit_themes(Admin_mission_theme_crudModel _Theme_CrudModels)
        {
            long themeId = (long)_Theme_CrudModels.themeId;
            MissionTheme themes = _adminrepository.GetThemeAdmin(themeId);
            themes.UpdatedAt = DateTime.Now;
            themes.Title = _Theme_CrudModels.themeTitle;
            themes.Status = (byte)_Theme_CrudModels.Status;
            _adminrepository.EditThemeAdmin(themes);
            _notyf.Success("theme edited successfully", 3);
            string searchText = "";
            int pageNumber = 1;
            int pageSize = 2;
            AdminPageList<MissionTheme> missionthemes = _adminrepository.GetMissionThemeAdmin(searchText, pageNumber, pageSize);
            return RedirectToAction("Admin_mission_theme", missionthemes);
        }
        [HttpPost]
        public IActionResult Admin_Delete_themes(long themeId)
        {
            MissionTheme themes = _adminrepository.GetThemeAdmin(themeId);
            themes.DeletedAt = DateTime.Now;
            _adminrepository.DeleteThemeAdmin(themes);
            string searchText = "";
            int pageNumber = 1;
            int pageSize = 2;
            AdminPageList<MissionTheme> missionthemes = _adminrepository.GetMissionThemeAdmin(searchText, pageNumber, pageSize);
            _notyf.Success("theme deleted successfully", 3);
            return PartialView("_Admin_Mission_theme", missionthemes);
        }

        //=========================================================================================================================
        ////mission Skill admin part crud
        //============================================================================================================================
        public IActionResult Admin_skill(Admin_skill_Model _Skill_Model)
        {
            string adminSessionEmailId = HttpContext.Session.GetString("useremail");

            if (adminSessionEmailId == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            string adminemail = adminSessionEmailId;
            User adminobj = _adminrepository.findadmin(adminemail);
            _Skill_Model.adminname = adminobj.FirstName + " " + adminobj.LastName;
            _Skill_Model.adminavatar = "./images/user1.png";
            return View(_Skill_Model);
        }
        [HttpPost]
        public IActionResult Admin_skill(string searchText, int pageNumber, int pageSize)
        {
            AdminPageList<Skill> skill = _adminrepository.GetSkillAdmin(searchText, pageNumber, pageSize);
            return PartialView("_Admin_skill_crud", skill);

        }
        public IActionResult Admin_Add_Skill()
        {
            Admin_skill_Model SkillModels = new Admin_skill_Model();
            string adminSessionEmailId = HttpContext.Session.GetString("useremail");

            if (adminSessionEmailId == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            else
            {
                string adminemail = adminSessionEmailId;
                User adminobj = _adminrepository.findadmin(adminemail);
                SkillModels.adminname = adminobj.FirstName + " " + adminobj.LastName;
                SkillModels.adminavatar = "./images/user1.png";
            }
            return View(SkillModels);
        }
        [HttpPost]
        public IActionResult Admin_Add_Skill(Admin_skill_Model SkillModels)
        {
            Skill skill = new Skill();
            skill.SkillName = SkillModels.SkillTitle;
            skill.CreatedAt = DateTime.Now;
            skill.Status = (byte)SkillModels.Status;
            _adminrepository.AddSkillAdmin(skill);
            _notyf.Success("skill added successfully", 3);
            return RedirectToAction("Admin_add_Skill", "Admin");
        }
        public IActionResult Admin_edit_skill(long skillId)
        {
            Admin_skill_Model SkillModels = new Admin_skill_Model();
            string adminSessionEmailId = HttpContext.Session.GetString("useremail");

            if (adminSessionEmailId == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            else
            {
                string adminemail = adminSessionEmailId;
                User adminobj = _adminrepository.findadmin(adminemail);
                SkillModels.adminname = adminobj.FirstName + " " + adminobj.LastName;
                SkillModels.adminavatar = "./images/user1.png";
                SkillModels.SkillId = skillId;
                Skill skills = _adminrepository.GetSkill(skillId);
                SkillModels.Status= skills.Status;
                SkillModels.SkillTitle = skills.SkillName;
            }
            return View(SkillModels);
        }
        [HttpPost]
        public IActionResult Admin_edit_Skill(Admin_skill_Model SkillModels)
        {
            long skillId = (long)SkillModels.SkillId;
            Skill skill = _adminrepository.GetSkill(skillId);
            skill.SkillName = SkillModels.SkillTitle;
            skill.UpdatedAt = DateTime.Now;
            skill.Status = (byte)SkillModels.Status;
            _adminrepository.EditSkill(skill);
            _notyf.Success("skill edited successfully", 3);
            return RedirectToAction("Admin_skill");
        }
        [HttpPost]
        public IActionResult Delete_Skill(int skillId)
        {
            Skill skill = _adminrepository.GetSkill(skillId);
            skill.DeletedAt = DateTime.Now;
            _adminrepository.DeleteSkill(skill);
            _notyf.Success("skill deleted successfully", 3);
            return RedirectToAction("Admin_skill");
        }
        //=========================================================================================================================
        ////Banner admin part crud
        //==================================================================================================================
        public IActionResult Admin_Banner(Admin_Banner_crudModel banner_CrudModel)
        {
            string adminSessionEmailId = HttpContext.Session.GetString("useremail");

            if (adminSessionEmailId == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            string adminemail = adminSessionEmailId;
            User adminobj = _adminrepository.findadmin(adminemail);
            banner_CrudModel.adminname = adminobj.FirstName + " " + adminobj.LastName;
            banner_CrudModel.adminavatar = "./images/user1.png";
            return View(banner_CrudModel);
        }
        [HttpPost]
        public IActionResult Admin_Banner(string searchText, int pageNumber, int pageSize)
        {
            AdminPageList<Banner> banner = _adminrepository.GetBanner(searchText, pageNumber, pageSize);
            return PartialView("Admin_Banner_crud", banner);

        }
        public IActionResult Admin_Add_Banner()
        {
            Admin_Banner_crudModel banner = new Admin_Banner_crudModel();
            string adminSessionEmailId = HttpContext.Session.GetString("useremail");

            if (adminSessionEmailId == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            string adminemail = adminSessionEmailId;
            User adminobj = _adminrepository.findadmin(adminemail);
            banner.adminname = adminobj.FirstName + " " + adminobj.LastName;
            banner.adminavatar = "./images/user1.png";
            return View(banner);

        }
        [HttpPost]
        public IActionResult Admin_Add_Banner(Admin_Banner_crudModel banner, IFormFile? filename)
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            User userObj = _userRepository.findUser(userSessionEmailId);
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            Banner banner1 = new Banner();
            if (filename != null)
            {
                string fileName = Guid.NewGuid().ToString();
                banner1.Image = fileName;
                var uploads = Path.Combine(wwwRootPath, @"images\Banner");
                var extension = Path.GetExtension(filename.FileName);
                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    filename.CopyTo(fileStreams);
                }
                banner1.Image = @"\images\Banner\" + fileName + extension;
            }
            banner1.Text = banner.text;

            banner1.CreatedAt = DateTime.Now;
            banner1.SortOrder = (int)banner.sortorder;
            _adminrepository.AddBannerAdmin(banner1);
            _notyf.Success("banner added successfully", 3);
            return RedirectToAction("Admin_Add_Banner", "Admin");
        }
        public IActionResult Admin_Edit_Banner(long bannerId)
        {
            Admin_Banner_crudModel banners = new Admin_Banner_crudModel();
            string adminSessionEmailId = HttpContext.Session.GetString("useremail");

            if (adminSessionEmailId == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            string adminemail = adminSessionEmailId;
            User adminobj = _adminrepository.findadmin(adminemail);
            banners.adminname = adminobj.FirstName + " " + adminobj.LastName;
            banners.adminavatar = "./images/user1.png";
            banners.bannerid = (int)bannerId;
            Banner banner1 = _adminrepository.GetBanner(bannerId);
            banners.sortorder = banner1.SortOrder;
            banners.text = banner1.Text;
            return View(banners);

        }
        [HttpPost]
        public IActionResult Admin_Edit_Banner(Admin_Banner_crudModel banners, IFormFile? filename)
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            User userObj = _userRepository.findUser(userSessionEmailId);
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            long bannerId = (long)banners.bannerid;
            Banner banner1 = _adminrepository.GetBanner(bannerId);
            if (filename != null)
            {
                string fileName = Guid.NewGuid().ToString();
                banner1.Image = fileName;
                var uploads = Path.Combine(wwwRootPath, @"images\Banner");
                var extension = Path.GetExtension(filename.FileName);
                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    filename.CopyTo(fileStreams);
                }
                banner1.Image = @"\images\Banner\" + fileName + extension;
            }
            banner1.Text = banners.text;

            banner1.UpdatedAt = DateTime.Now;
            banner1.SortOrder = (int)banners.sortorder;
            _adminrepository.EditBannerAdmin(banner1);
            string searchText = "";
            int pageNumber = 1;
            int pageSize = 2;
            AdminPageList<Banner> banner = _adminrepository.GetBanner(searchText, pageNumber, pageSize);
            _notyf.Success("banner edited successfully", 3);
            return RedirectToAction("Admin_Banner", banner);
        }
        [HttpPost]
        public IActionResult Delete_Banner(int bannerId)
        {
            Banner banner1 = _adminrepository.GetBanner(bannerId);
            banner1.DeletedAt = DateTime.Now;
            _adminrepository.DeleteBannerAdmin(banner1);
            _notyf.Success("banner deleted successfully", 3);
            return RedirectToAction("Admin_Banner");
        }
        //=========================================================================================================================
        ///Mission admin part crud
        //==================================================================================================================
        public IActionResult Admin_mission(Admin_Mission_crudModel mission_crud)
        {
            string adminSessionEmailId = HttpContext.Session.GetString("useremail");

            if (adminSessionEmailId == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            string adminemail = adminSessionEmailId;
            User adminobj = _adminrepository.findadmin(adminemail);
            mission_crud.adminname = adminobj.FirstName + " " + adminobj.LastName;
            mission_crud.adminavatar = "./images/user1.png";
            return View(mission_crud);
        }
        [HttpPost]
        public IActionResult Admin_mission(string searchText, int pageNumber, int pageSize)
        {
            AdminPageList<Mission> Missions = _adminrepository.GetMissionAdmin(searchText, pageNumber, pageSize);
            return PartialView("Admin_Mission_crud", Missions);

        }
        public IActionResult Admin_Add_Mission()
        {
            Admin_Mission_crudModel mission_crud = new Admin_Mission_crudModel();
            string adminSessionEmailId = HttpContext.Session.GetString("useremail");

            if (adminSessionEmailId == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            string adminemail = adminSessionEmailId;
            User adminobj = _adminrepository.findadmin(adminemail);
            mission_crud.adminname = adminobj.FirstName + " " + adminobj.LastName;
            mission_crud.adminavatar = "./images/user1.png";
            return View(mission_crud);
        }
        [HttpPost]
        public IActionResult Admin_Add_Mission(Admin_Mission_crudModel missionModels, List<IFormFile>? filename,List<IFormFile>? documentfilename)
        {
            Mission mission = new Mission();
            mission.Title = missionModels.missiontitle;
            mission.ShortDescription = missionModels.shortdescription;
            mission.Description = missionModels.longdescription;
            mission.StartDate = missionModels.startdate;
            mission.EndDate = missionModels.enddate;
            mission.MissionType = missionModels.missiontype;
            mission.OrganizationName = missionModels.organizationname;
            mission.OrganizationDetail = missionModels.organizationdescription;
            mission.CreatedAt = DateTime.Now;
            mission.Availability = (int)missionModels.avaliblity;
            mission.CountryId = (long)missionModels.country;
            mission.CityId = (long)missionModels.city;
            mission.ThemeId = (long)missionModels.missiontheme;
            long missionid = _adminrepository.AddMission(mission);
            _notyf.Success("mission added successfully", 3);
            List<MissionSkill> missionSkillsList=new List<MissionSkill>();
            if (missionModels.missionskills != null)
            {
                string[] skills = missionModels.missionskills.Replace("\r", "").Split("\n").SkipLast(1).ToArray();
                foreach (var skill in skills)
                {
                    int SkillId = _adminrepository.GetSkillvianame(skill);
                    MissionSkill mSkill = new MissionSkill();
                    mSkill.SkillId = SkillId;
                    mSkill.MissionId = missionid;
                    _adminrepository.AddMissionSkill(mSkill);
                    missionSkillsList.Add(mSkill);
                }

            }
            if (missionModels.missiontype == "goal")
            {
                GoalMission goalMission = new GoalMission();
                goalMission.MissionId = missionid;
                goalMission.CreatedAt = DateTime.Now;
                goalMission.GoalValue = (int)missionModels.goalvalue;
                goalMission.GoalObjectiveText = missionModels.goalobjective;
                _adminrepository.AddGoalMission(goalMission);
            }
            if (filename != null)
            {
                MissionMedium media = new MissionMedium();
                media.MissionId = missionid;
                string name = "";
                string path = "";
                string type = "";
                foreach (var files in filename)
                {
                    string userSessionEmailId = HttpContext.Session.GetString("useremail");
                    User userObj = _userRepository.findUser(userSessionEmailId);
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\media\");
                    var extension = Path.GetExtension(files.FileName);
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files.CopyTo(fileStreams);
                    }

                    name += fileName + ",";
                    type = "png";
                    path = @"images/media";
                }
                int index = name.LastIndexOf(',');
                media.MediaName = name.Substring(0, index);
                media.MediaType = type;
                media.MediaPath = path;
                _adminrepository.AddMissionMedia(media);


                _notyf.Success("mission media added successfully", 3);

            }
            if (documentfilename.Count != 0)
            {
                MissionDocument missionDocument =new MissionDocument();
                missionDocument.MissionId = missionid;
                string doctype = "";
                string docname = "";
                foreach (var docfile in documentfilename)
                {
                    string userSessionEmailId = HttpContext.Session.GetString("useremail");
                    User userObj = _userRepository.findUser(userSessionEmailId);
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    missionDocument.UpdatedAt = DateTime.Now;
                    string documentfilenames = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"document\");
                    var extension = Path.GetExtension(docfile.FileName);
                    using (var fileStreams = new FileStream(Path.Combine(uploads, documentfilenames + extension), FileMode.Create))
                    {
                        docfile.CopyTo(fileStreams);
                    }
                    docname += documentfilenames + ",";
                    doctype += extension + ",";
                }
                int index = docname.LastIndexOf(',');
                missionDocument.DocumentName = docname.Substring(0, index);
                int indexs = doctype.LastIndexOf(',');
                missionDocument.DocumentType = doctype.Substring(0, indexs);
                missionDocument.DocumentPath = "document";
                _adminrepository.AddMissionDocument(missionDocument);
                _notyf.Success("mission document added successfully", 3);
            }
            List<User> users = _adminrepository.GetUserWithSkillAvailability(missionSkillsList);
            if(users != null)
            {
                foreach(User user in users)
                {
                    _adminrepository.GiveMissionNotification(user,missionid);
                }
            }
            return RedirectToAction("Admin_Add_Mission");
        }
        public IActionResult Admin_Edit_Mission(long missionId, IFormFile? filename, IFormFile? documentfilename)
        {
            Admin_Mission_crudModel mission_crud = new Admin_Mission_crudModel();
            string adminSessionEmailId = HttpContext.Session.GetString("useremail");

            if (adminSessionEmailId == null)
            {
                return RedirectToAction("Admin_Login", "Admin");
            }
            string adminemail = adminSessionEmailId;
            User adminobj = _adminrepository.findadmin(adminemail);
            mission_crud.adminname = adminobj.FirstName + " " + adminobj.LastName;
            mission_crud.adminavatar = "./images/user1.png";
            mission_crud.missionid = missionId;
            Mission mission = _adminrepository.GetMission(missionId);
            mission_crud.missiontitle = mission.Title;
            mission_crud.shortdescription = mission.ShortDescription;
            mission_crud.longdescription = mission.Description;
            mission_crud.startdate = mission.StartDate;
            mission_crud.enddate = mission.EndDate;
            mission_crud.missiontype = mission.MissionType;
            mission_crud.organizationname = mission.OrganizationName;
            mission_crud.organizationdescription = mission.OrganizationDetail;
            if (mission_crud.missiontype == "goal")
            {
                long goalId = missionId;
                GoalMission goal = _adminrepository.GetGoalMission(goalId);
                mission_crud.goalobjective = goal.GoalObjectiveText;
                mission_crud.goalvalue = goal.GoalValue;
            }
            MissionMedium media = _adminrepository.GetMissionMedium(missionId);
            if (media != null)
            {
                mission_crud.filename = media.MediaName;
                mission_crud.filetype = media.MediaType;
                mission_crud.filepath = media.MediaPath;
            }
            //MissionDocument missionDocument = _adminrepository.GetMissionDocument(missionId);
            //if (missionDocument != null)
            //{

            //}
           
            List<MissionSkill> msskill= _adminrepository.missionSkills(missionId);
            if(msskill.Count() !=0)
            {
                string missionskillname = "";
                foreach (MissionSkill skill in msskill)
                {
                    long skillId = skill.SkillId;
                   Skill skill1= _adminrepository.GetSkillName(skillId);
                    string skillname = skill1.SkillName;
                    missionskillname += skill.Skill.SkillName + "\n";
                }
                mission_crud.missionskills = missionskillname;
            }
            return View(mission_crud);
        }
        [HttpPost]
        public IActionResult Admin_Edit_Mission(Admin_Mission_crudModel admin_Mission_, List<IFormFile>? filename, List<IFormFile>? documentfilename)
        {
            long missionId = (long)admin_Mission_.missionid;
            Mission mission = _adminrepository.GetMission(missionId);
            mission.Title = admin_Mission_.missiontitle;
            mission.ShortDescription = admin_Mission_.shortdescription;
            mission.Description = admin_Mission_.longdescription;
            mission.StartDate = admin_Mission_.startdate;
            mission.EndDate = admin_Mission_.enddate;
            mission.MissionType = admin_Mission_.missiontype;
            mission.OrganizationName = admin_Mission_.organizationname;
            mission.OrganizationDetail = admin_Mission_.organizationdescription;
            mission.CreatedAt = DateTime.Now;
            mission.Availability = (int)admin_Mission_.avaliblity;
            mission.CountryId = (long)admin_Mission_.country;
            mission.CityId = (long)admin_Mission_.city;
            mission.ThemeId = (long)admin_Mission_.missiontheme;
            _adminrepository.EditMission(mission);
            if (admin_Mission_.missionskills != null)
            {
                _adminrepository.RemoveMissionSkill(missionId);
                string[] skills = admin_Mission_.missionskills.Replace("\r", "").Split("\n").SkipLast(1).ToArray();
                foreach (var skill in skills)
                {                   
                    int SkillId = _adminrepository.GetSkillvianame(skill);
                    MissionSkill mSkill = new MissionSkill();
                    mSkill.SkillId = SkillId;
                    mSkill.MissionId = admin_Mission_.missionid;
                    _adminrepository.AddMissionSkill(mSkill);

                }
            }
            MissionMedium media = _adminrepository.GetMissionMedium(missionId);
            if (filename.Count != 0)
            {             
                media.UpdatedAt = DateTime.Now;
                long MissionId = (long)admin_Mission_.missionid;
                string name = "";
                string path = "";
                string type = "";
                foreach (var files in filename)
                {
                    string userSessionEmailId = HttpContext.Session.GetString("useremail");
                    User userObj = _userRepository.findUser(userSessionEmailId);
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\media\");
                    var extension = Path.GetExtension(files.FileName);
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files.CopyTo(fileStreams);
                    }

                    name += fileName + ",";
                    type = "png";
                    path = @"images/media";
                }
                int index = name.LastIndexOf(',');
                media.MediaName = name.Substring(0, index);
                media.MediaPath = path;
                media.MediaType = type;
                _adminrepository.UpdateMissionMedia(media);
            }

  
            if (documentfilename.Count !=0 )
            {
                MissionDocument missionDocument = new MissionDocument();
                missionDocument.MissionId = missionId;
                string doctype = "";
                string docname = "";
                foreach (var docfile in documentfilename)
                {
                    string userSessionEmailId = HttpContext.Session.GetString("useremail");
                    User userObj = _userRepository.findUser(userSessionEmailId);
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    missionDocument.UpdatedAt = DateTime.Now;
                    string documentfilenames = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"document\");
                    var extension = Path.GetExtension(docfile.FileName);
                    using (var fileStreams = new FileStream(Path.Combine(uploads, documentfilenames + extension), FileMode.Create))
                    {
                        docfile.CopyTo(fileStreams);
                    }
                    docname += documentfilenames + ",";
                    doctype += extension + ",";
                }
                int index = docname.LastIndexOf(',');
                missionDocument.DocumentName = docname.Substring(0, index);
                int indexs = doctype.LastIndexOf(',');
                missionDocument.DocumentType = doctype.Substring(0, indexs);
                missionDocument.DocumentPath = "document";
                _adminrepository.UpdateMissionDocument(missionDocument);
            }

            string searchText = "";
            int pageNumber = 1;
            int pageSize = 2;
            AdminPageList<Mission> missionget = _adminrepository.GetMissionAdmin(searchText, pageNumber, pageSize);
            return RedirectToAction("Admin_mission", missionget);
        }

        //===================================
        //get  data of mission page
        //===================================
        public IActionResult GetCountries()
        {
            IEnumerable<Country> countries = _userRepository.getCountries();
            return Json(new { data = countries });
        }
        public IActionResult GetCities()
        {
            IEnumerable<City> cities = _userRepository.getCities();
            return Json(new { data = cities });
        }
        [HttpPost]
        public IActionResult GetCities(long countryid)
        {
            IEnumerable<City> cities = _userRepository.getCities(countryid);
            return Json(new { data = cities });
        }
        public IActionResult GetMissionTheme()
        {
            IEnumerable<MissionTheme> themes = _adminrepository.GetMissionTheme();
            return Json(new { data = themes });
        }
        public IActionResult GetSkills()
        {
            IEnumerable<Skill> skill = _userRepository.getSkill();
            return Json(new { data = skill });
        }
        [HttpPost]
        public IActionResult Delete_Mission(int missionId)
        {
            Mission mission = _adminrepository.GetMission(missionId);
            mission.DeletedAt = DateTime.Now;
            _adminrepository.DeleteMission(mission);
            _notyf.Success("mission deleted successfully", 3);
            return RedirectToAction("Admin_mission");
        }

    }
}
