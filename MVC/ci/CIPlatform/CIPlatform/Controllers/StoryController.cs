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
        public StoryController(IStoryRepository storyRepository,IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
            _storyRepository = storyRepository;
        }
        public IActionResult StoryListing()
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");

            if (userSessionEmailId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            HomeModel homeModel = new HomeModel();  
            string userSession = HttpContext.Session.GetString("useremail");
            User userObj = _homeRepository.getuser(userSession);
            homeModel.username = userObj.FirstName + " " + userObj.LastName;
            homeModel.id = userObj.UserId;
            homeModel.avatar = userObj.Avatar.ToString();

            return View(homeModel);
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

    }
}

