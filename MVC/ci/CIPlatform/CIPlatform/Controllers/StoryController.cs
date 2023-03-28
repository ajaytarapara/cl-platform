using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
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

        public StoryController(IStoryRepository storyRepository,IHomeRepository homeRepository,IUserRepository userRepository)
        {
            _homeRepository = homeRepository;
            _storyRepository = storyRepository;
            _userRepository = userRepository;
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

            HomeModel HomeModel = new HomeModel();

            User userObj = _userRepository.findUser(userSessionEmailId);
            HomeModel.username = userObj.FirstName + " " + userObj.LastName;
            HomeModel.id = userObj.UserId;
            ShareStoryModel ShareStoryModel = new ShareStoryModel();
            ShareStoryModel.userid = HomeModel.id;
            ShareStoryModel.username = HomeModel.username;
            ShareStoryModel.avatar = HomeModel.avatar;
            long UserId = ShareStoryModel.userid;
            ShareStoryModel.getmission = _storyRepository.Getstorymission(UserId);
            return View(ShareStoryModel);
        }

        [HttpPost]
        public void Savestory(ShareStoryModel storymodel)
        {
            Story story = new Story();
            story.UserId = storymodel.userid;
            story.MissionId =(long) storymodel.MissionId;
            story.Title = storymodel.Title;
            story.Description = storymodel.Description;
            story.PublishedAt = storymodel.PublishedAt;
          _storyRepository.Savestory(story);

        }

    }
}

