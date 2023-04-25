﻿using CIPlatform.Repository.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.DataModels;
using MailKit.Net.Smtp;
using MimeKit;
using CIPlatform.Helpers;
using System.Collections;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using AspNetCoreHero.ToastNotification.Abstractions;
using CIPlatform.Entities.Auth;
using CIPlatform.Auth;

namespace CIPlatform.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration configuration;
        private readonly IHomeRepository _homeRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly INotyfService _notyf;
        public AccountController(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, IConfiguration _configuration
, IHomeRepository homeRepository, IWebHostEnvironment webHostEnvironment, INotyfService notyf)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            configuration = _configuration;
            _homeRepository = homeRepository;
            _webHostEnvironment = webHostEnvironment;
            _notyf = notyf;
        }
        public IActionResult Login()
        {
            LoginModel model = new LoginModel();
            model.Banner = _userRepository.getbanner();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel obj)
        {
            string emailId = obj.EmailId;
            string password = obj.Password;
            Boolean isValidEmail = _userRepository.validateEmail(emailId);
            if (!isValidEmail)
            {
                ModelState.AddModelError("EmailId", "Email not found");
            }
            else
            {
                User isValidUser = _userRepository.validateUser(emailId, password);
                if (isValidUser == null)
                {
                    ModelState.AddModelError("Password", "Password does not match");
                }
            }
            if (ModelState.IsValid)
            {
                User isValidUser = _userRepository.validateUser(emailId, password);
                SessionDetailsViewModel sessionDetailsViewModel = new SessionDetailsViewModel();
                sessionDetailsViewModel.Email = isValidUser.Email;
                sessionDetailsViewModel.Avatar = isValidUser.Avatar;
                sessionDetailsViewModel.UserId = isValidUser.UserId;
                sessionDetailsViewModel.FullName = isValidUser.FirstName + " " + isValidUser.LastName;
                sessionDetailsViewModel.Role = isValidUser.Role;
                var jwtSetting = configuration.GetSection(nameof(JwtSetting)).Get<JwtSetting>();
                var token = JwtTokenHelper.GenerateToken(jwtSetting, sessionDetailsViewModel);
                if (string.IsNullOrWhiteSpace(token))
                {
                    ModelState.AddModelError("email", "Enter correct email");
                    return View("Login");
                }

                HttpContext.Session.SetString("Token", token);
                HttpContext.Session.SetString("useremail", emailId);
                if (isValidUser.Role == "volunteer")
                {
                    _notyf.Success("LoginSuccessFully",3);
                    return RedirectToAction("Index", "Home");
                }
                if (isValidUser.Role == "admin")
                    return RedirectToAction("User_crud", "Admin");
                //return RedirectToAction("Index", "Home");
            }
            return Login();
        }
        public IActionResult ForgotPassword()
        {
            ForgotPasswordModel model = new ForgotPasswordModel();
            model.Banner = _userRepository.getbanner();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult ForgotPassword(ForgotPasswordModel obj)
        {
            if (!_userRepository.validateEmail(obj.EmailId))
            {
                ModelState.AddModelError("EmailId", "Email not found");
            }
            if (ModelState.IsValid)
            {

                string uuid = Guid.NewGuid().ToString();
                PasswordReset resetPasswordObj = new PasswordReset();
                resetPasswordObj.Email = obj.EmailId;
                resetPasswordObj.Token = uuid;
                resetPasswordObj.CreatedAt = DateTime.Now;

                _userRepository.addResetPasswordToken(resetPasswordObj);

                var userObj = _userRepository.findUser(obj.EmailId);
                int UserID = (int)userObj.UserId;
                string welcomeMessage = "Welcome to CI platform, <br/> You can Reset your password using below link. </br>";
                string path = "<a href=\"" + " https://" + _httpContextAccessor.HttpContext.Request.Host.Value + "/Account/NewPassword?token=" + uuid + " \"  style=\"font-weight:500;color:blue;\" > Reset Password </a>";
                MailHelper mailHelper = new MailHelper(configuration);
                ViewBag.sendMail = mailHelper.Send(obj.EmailId, welcomeMessage + path);


                return View();
            }
            return View();
        }
        public IActionResult Register()
        {
            RegistrationModel model = new RegistrationModel();
            model.Banner = _userRepository.getbanner();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegistrationModel obj)
        {
            if (_userRepository.validateEmail(obj.EmailId))
            {
                ModelState.AddModelError("EmailId", "Email already registred");
            }
            else
            {
                if (!obj.Password.Equals(obj.ConfirmPassword))
                {
                    ModelState.AddModelError("ConfirmPassword", "Confirm password does not match to new password");
                }
            }
            if (ModelState.IsValid)
            {
                User user = new User();
                user.FirstName = obj.FirstName;
                user.LastName = obj.LastName;
                user.PhoneNumber = long.Parse(obj.PhoneNo);
                user.Email = obj.EmailId;
                user.Password = obj.Password;
                user.Avatar = "/images/user1.png";
                _userRepository.addUser(user);
                return RedirectToAction("Login");
            }
            return View();
        }

        public IActionResult NewPassword(string? token)
        {

            var resetObj = _userRepository.findUserByToken(token);
            TimeSpan remainingTime = DateTime.Now - resetObj.CreatedAt;

            int hour = remainingTime.Hours;

            if (hour >= 4)
            {
                _userRepository.removeResetPasswordToken(resetObj);
                return RedirectToAction("Login");
            }
            NewPasswordModel newPasswordModel = new NewPasswordModel();
            newPasswordModel.token = token;
            newPasswordModel.Banner = _userRepository.getbanner();
            return View(newPasswordModel);
        }

        [HttpPost]
        public IActionResult NewPassword(NewPasswordModel obj)
        {
            string token = obj.token;
            if (token != null)
            {
                if (obj.NewPassword.Equals(obj.ConfirmPassword))
                {

                    var resetObj = _userRepository.findUserByToken(token);
                    var userObj = _userRepository.findUser(resetObj.Email);
                    if (!obj.NewPassword.Equals(userObj.Password))
                    {
                        userObj.Password = obj.NewPassword;
                        _userRepository.updatePassword(userObj);
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ModelState.AddModelError("NewPassword", "You can not set Old password as New Password");
                    }
                }
                else
                {
                    ModelState.AddModelError("ConfirmPassword", "Confirm password does not match to new password");
                }
            }
            return View();
        }

        public IActionResult EditProfile()
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");

            if (userSessionEmailId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            EditProfileModel editProfile = new EditProfileModel();

            User userObj = _userRepository.findUser(userSessionEmailId);
            editProfile.firstname = userObj.FirstName;
            editProfile.lastname = userObj.LastName;
            editProfile.avatar = userObj.Avatar;
            editProfile.userid = userObj.UserId;
            editProfile.useremail = userObj.Email;
            editProfile.countrofuser = userObj.CountryId;
            editProfile.title = userObj.Title;
            editProfile.whyivol = userObj.WhyIVolunteer;
            editProfile.employeeid = userObj.EmployeeId;
            editProfile.linkedinurl = userObj.LinkedInUrl;
            editProfile.department = userObj.Department;
            editProfile.profiletext = userObj.ProfileText;
            return View(editProfile);
        }

        [HttpPost]
        public IActionResult EditProfile(EditProfileModel user, IFormFile? filename)
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            User userObj = _userRepository.findUser(userSessionEmailId);
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (filename != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"images\avatars");
                var extension = Path.GetExtension(filename.FileName);
                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    filename.CopyTo(fileStreams);
                }
                userObj.Avatar = @"\images\avatars\" + fileName + extension;
            }
            userObj.FirstName = user.firstname;
            userObj.LastName = user.lastname;
            userObj.Department = user.department;
            userObj.ProfileText = user.profiletext;
            userObj.WhyIVolunteer = user.whyivol;
            userObj.CityId = user.cityofuser;
            userObj.CountryId = user.countrofuser;
            userObj.LinkedInUrl = user.linkedinurl;
            userObj.EmployeeId = user.employeeid;
            userObj.ProfileText = user.profiletext;
            userObj.Title = user.title;
            userObj.UpdatedAt = DateTime.Now;
            List<UserSkill> userSkill = new List<UserSkill>();
            string[] skills = user.userskills.Replace("\r", "").Split("\n").SkipLast(1).ToArray();
            foreach (var skill in skills)
            {
                UserSkill skill1 = new UserSkill();
                skill1.SkillId = _userRepository.getskillid(skill);
                skill1.UserId = userObj.UserId;
                userSkill.Add(skill1);
            }

            _userRepository.edituserprofile(userObj, userSkill);
            _notyf.Success("Your Profile updated Successfully", 3);
            return RedirectToAction("EditProfile");

        }

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

        public IActionResult GetSkills()
        {
            IEnumerable<Skill> skill = _userRepository.getSkill();
            return Json(new { data = skill });
        }

        [HttpPost]
        public IActionResult EditPassword(EditProfileModel editPassword)
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            User userObj = _userRepository.findUser(userSessionEmailId);
            editPassword.email = userObj.Email;
            if (editPassword.password.Oldpassword.Equals(userObj.Password))
            {
                if (editPassword.password.Newpassword.Equals(editPassword.password.ConfirmPassword))
                {
                    if (editPassword.password.Newpassword.Equals(userObj.Password))
                    {
                        _notyf.Error("you can not set new pass equal old pass", 3);
                        ModelState.AddModelError("oldpassword", "you can not set new pass equal old pass");
                    }
                    else
                    {
                        userObj.Password = editPassword.password.Newpassword;
                        _userRepository.editPassword(userObj);
                        _notyf.Success("Password Updated Successfully", 3);

                    }

                }
                else
                {
                    _notyf.Error("Confirm password does not match to new password", 3);
                    ModelState.AddModelError("ConfirmPassword", "Confirm password does not match to new password");
                }
            }
            else
            {
                _notyf.Error("old password is does not match ", 3);
                ModelState.AddModelError("oldpassword", "old password is does not match ");
            }
            return RedirectToAction("EditProfile");
        }

        [HttpPost]
        public IActionResult contactus(EditProfileModel profileModel)
        {
            var adminemail = "ajaytarapara77@gmail.com";
            string welcomeMessage = "Welcome to CI platform, <br/>Volunteer want to contact /n </br>";
            var message = profileModel.message;
            var subject = profileModel.subject;
            MailHelper mailHelper = new MailHelper(configuration);
            ViewBag.sendMail = mailHelper.Send(adminemail, welcomeMessage + message, subject);
            _notyf.Error("mail sended successfully", 3);
            return RedirectToAction("EditProfile");

        }

        public IActionResult logout()
        {
            HttpContext.Session.Remove("useremail");
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult VolunteeringTimesheet()
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            if (userSessionEmailId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            User userObj = _userRepository.findUser(userSessionEmailId);
            VolunteeringTimesheetModel timesheetModel = new VolunteeringTimesheetModel();
            timesheetModel.username = userObj.FirstName + " " + userObj.LastName;
            timesheetModel.avatar = userObj.Avatar;
            long UserId = userObj.UserId;
            timesheetModel.timesheet = _userRepository.getTimesheets(UserId);
            timesheetModel.missiontitle = _userRepository.getmissiontitle(UserId);
            return View(timesheetModel);
        }

        [HttpPost]
        public IActionResult addtimesheet(long MissionId, string DateVolunteered, string Notes, string hours, string minutes)
        {
            Timesheet timesheet = new Timesheet();
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            User userObj = _userRepository.findUser(userSessionEmailId);
            timesheet.UserId = userObj.UserId;
            timesheet.MissionId = MissionId;
            timesheet.Notes = Notes;
            if (hours != null && minutes != null && DateVolunteered != null)
            {
                timesheet.DateVolunteered = DateTime.Parse(DateVolunteered);
                timesheet.Time = TimeOnly.Parse(hours + ":" + minutes);
                timesheet.Status = "approved";
                _userRepository.addtimesheet(timesheet);
                _notyf.Success("time sheet added successfully", 3);
                return Json(new { status = 1 });
            }
            else
            {
                _notyf.Error("time is required", 3);
                ModelState.AddModelError("time", "time is required");
                return Json(new { status = 0 });
            }


        }

        [HttpPost]
        public IActionResult addtimesheetgoal(long MissionId, string DateVolunteered, string Notes, string Action)
        {
            Timesheet timesheet = new Timesheet();
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            User userObj = _userRepository.findUser(userSessionEmailId);
            timesheet.UserId = userObj.UserId;
            timesheet.MissionId = MissionId;
            timesheet.Notes = Notes;
            if (Action != null && DateVolunteered != null)
            {
                timesheet.Action = int.Parse(Action);
                timesheet.DateVolunteered = DateTime.Parse(DateVolunteered);
                timesheet.Status = "approved";
                _userRepository.addtimesheet(timesheet);
                _notyf.Success("time sheet deleted successfully", 3);

                return Json(new { status = 1 });
            }
            else
            {
                _notyf.Error("time sheet not added successfully", 3);
                ModelState.AddModelError("data", "data is not valid");
                return Json(new { status = 0 });
            }

        }

        [HttpPost]
        public IActionResult deletetimesheet(long timesheetid)
        {
            Timesheet timesheet = new Timesheet();
            timesheet.TimesheetId = timesheetid;
            _userRepository.deletetimesheet(timesheet);
            _notyf.Success("time sheet deleted successfully", 3);
            return RedirectToAction("VolunteeringTimesheet", "Account");
        }

        [HttpPost]
        public IActionResult deletetimesheetgoal(long timesheetid)
        {
            Timesheet timesheet = new Timesheet();
            timesheet.TimesheetId = timesheetid;
            _userRepository.deletetimesheet(timesheet);
            _notyf.Success("time sheet deleted successfully", 3);
            return RedirectToAction("VolunteeringTimesheet", "Account");
        }

        [HttpPost]
        public IActionResult edittimesheet(long timesheetid, string hours, string minutes, long MissionId, string Notes, string DateVolunteered)
        {
            Timesheet timesheet = new Timesheet();
            timesheet.Notes = Notes;
            if (hours != null && minutes != null && Notes != null && DateVolunteered != null)
            {
                if (Int32.Parse(minutes) > 60)
                {

                    return Json(new { status = 2 });
                }
                timesheet.DateVolunteered = DateTime.Parse(DateVolunteered);
                timesheet.Time = TimeOnly.Parse(hours + ":" + minutes);
                _userRepository.edittimesheet(timesheetid, hours, minutes, MissionId, Notes, DateVolunteered);
                _notyf.Success("time sheet upadeted successfully", 3);
                return Json(new { status = 1 });
            }
            else
            {
                _notyf.Error("time sheet not upadeted successfully", 3);
                ModelState.AddModelError("data", "data is not valid");
                return Json(new { status = 0 });
            }
        }

        [HttpPost]
        public IActionResult edittimesheetgoal(long timesheetid, long MissionId, string Notes, long Action, string DateVolunteered)
        {
            if (Notes != null && Action != 0 && DateVolunteered != null)
            {
                _userRepository.edittimesheetgoal(timesheetid, MissionId, Notes, Action, DateVolunteered);
                _notyf.Error("time sheet  upadeted successfully", 3);
                return Json(new { status = 1 });
            }
            else
            {
                _notyf.Error("time sheet not upadeted successfully", 3);
                ModelState.AddModelError("data", "data is not valid");
                return Json(new { status = 0 });
            }
        }

        public IActionResult Privacy_policy()
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            if (userSessionEmailId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            User userObj = _userRepository.findUser(userSessionEmailId);
            HomeModel privacy = new HomeModel();
            privacy.username = userObj.FirstName + " " + userObj.LastName;
            privacy.avatar = userObj.Avatar;
            return View(privacy);
        }
    }
}