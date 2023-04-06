using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Helpers;
using CIPlatform.Repository.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatform.Controllers
{
    public class StoryController : Controller
    {
        private readonly IStoryRepository _storyRepository;
        private readonly IHomeRepository _homeRepository;
        private readonly IUserRepository _userRepository;
        private readonly string userSessionEmailId;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public StoryController(IStoryRepository storyRepository,IHomeRepository homeRepository,IUserRepository userRepository, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment, IConfiguration _configuration, IHttpContextAccessor httpContextAccessor)
        {
            _homeRepository = homeRepository;
            _storyRepository = storyRepository;
            _userRepository = userRepository;
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
            configuration = _configuration;
        }
        public IActionResult StoryListing()
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");

            if (userSessionEmailId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            HomeModel HomeModel = new HomeModel();
            User userObj = _userRepository.findUser(userSessionEmailId);
            HomeModel.username = userObj.FirstName + " " + userObj.LastName;
            HomeModel.id = userObj.UserId;
            HomeModel.avatar=userObj.Avatar;
            return View(HomeModel);
        }
        public IActionResult GetCountries()
        {
            IEnumerable<Country> countries = _storyRepository.getCountries();
            return Json(new { data = countries });
        }
        public IActionResult GetCities()
        {
            IEnumerable<City> cities = _storyRepository.getCities();
           return Json(new { data = cities });
        }
        public IActionResult GetThemes()
        {
            IEnumerable<MissionTheme> missionThemes = _storyRepository.getThemes();
            return Json(new { data = missionThemes });
        }
        public IActionResult GetSkills()
        {
           IEnumerable<Skill> missionSkills = _storyRepository.getSkills();
            return Json(new { data = missionSkills });
        }
        public IActionResult Storydata(int pageNumber)
        {
            string userSession = HttpContext.Session.GetString("useremail");
            User userObj = _homeRepository.getuser(userSession);
            PaginationMission story =_storyRepository.Storydata(pageNumber);   
            return PartialView("_Storylist", story);
        }

        public IActionResult Share_story()
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");

            if (userSessionEmailId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var missions = _homeRepository.GetMissions();

            HomeModel HomeModels = new HomeModel();

            User userObj = _userRepository.findUser(userSessionEmailId);
            HomeModels.username = userObj.FirstName + " " + userObj.LastName;
            HomeModels.id = userObj.UserId;
            HomeModels.avatar=userObj.Avatar;   
            ShareStoryModel ShareStoryModel = new ShareStoryModel();
            ShareStoryModel.userid = HomeModels.id;
            ShareStoryModel.username = HomeModels.username;
            ShareStoryModel.avatar = HomeModels.avatar;
            long UserId = ShareStoryModel.userid;
            ShareStoryModel.getmission = _storyRepository.Getstorymission(UserId);
            return View(ShareStoryModel);
        }

        [HttpPost]
        public IActionResult Savestory(ShareStoryModel storymodel)
        {
            Story story = new Story();
            story.UserId = storymodel.userid;
            story.MissionId = (long)storymodel.MissionId;
            story.Title = storymodel.Title;
            story.Description = storymodel.Description;
            story.PublishedAt = storymodel.PublishedAt;
             StoryMedium storymedia = new StoryMedium();
            storymedia.Path = storymodel.StoryMedia;
            int storyId = _storyRepository.Savestory(story, storymedia); 
            return Json(new { storyId = storyId });
        }

        public IActionResult Upload(List<IFormFile> postedFiles)
        {
            string wwwPath = this._hostingEnvironment.WebRootPath;
            string contentPath = this._hostingEnvironment.ContentRootPath;

            string path = Path.Combine(this._hostingEnvironment.WebRootPath, @"images\uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory("postedFiles");
            }

            foreach (IFormFile postedFile in postedFiles)
            {
                string fileName = Path.GetFileName(postedFile.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
            }

            return Content("Success");
        }

        public IActionResult View_Story(long storyid,Story story)
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");

            if (userSessionEmailId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            HomeModel HomeModel = new HomeModel();

            User userObj = _userRepository.findUser(userSessionEmailId);
            HomeModel.username = userObj.FirstName + " " + userObj.LastName;
            HomeModel.id = userObj.UserId;
            HomeModel.avatar = userObj.Avatar;
            StoryDetailModel model = new StoryDetailModel();
            model.navusername = HomeModel.username;
            model.navavatar = HomeModel.avatar;
            model.story= _storyRepository.Getdetailstory(story, storyid);
            model.usersemails = _storyRepository.Getusersemail();
            return View(model);
        }

        [HttpPost]
        public void recommendedtocoworker(string cow_email, int Missionid)
        {
            string userSession = HttpContext.Session.GetString("useremail");
            User userObj = _homeRepository.getuser(userSession);
            string welcomeMessage = "Welcome to CI platform, <br/> You can participate in mission using below link. </br>";
            string path = "<a href=\"" + " https://" + _httpContextAccessor.HttpContext.Request.Host.Value + "/Mission/Mission_Volunteer?id=" + Missionid.ToString() + " \"  style=\"font-weight:500;color:blue;\" > Apply to Mission </a>";
            string subject = "Your Friend recommanded for volunteering";
            MailHelper mailHelper = new MailHelper(configuration);
            ViewBag.sendMail = mailHelper.Send(cow_email, welcomeMessage + path,subject);

        }




    }
}

