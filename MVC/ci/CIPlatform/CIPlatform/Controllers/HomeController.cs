//using CIPlatform.Entities.DataModels;
//using CIPlatform.Entities.ViewModels;
//using CIPlatform.Models;
//using CIPlatform.Repository.Repository.Interface;
//using Microsoft.AspNetCore.Mvc;
//using System.Diagnostics;

//namespace CIPlatform.Controllers
//{
//    public class HomeController : Controller
//    {
//        private readonly IUserRepository _userRepository;
//        private readonly IHomeRepository _homerepository;

//        public HomeController(IUserRepository userRepository, IHomeRepository homeRepository  )
//        {
//            _userRepository = userRepository;
//            _homerepository = homeRepository;
//        }
//        public IActionResult Index(HomeModel obj)
//            {
//                HomeModel HomeModel = new HomeModel();
//                string email = "sagar@gmail.com";
//                var finduser = _userRepository.findUser(email);

//                HomeModel.username = finduser.FirstName + " " + finduser.LastName;

//                return View(HomeModel);
//            }


//        public IActionResult Privacy()
//        {
//            return View();
//        }

//    }
//}
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Repository.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CIPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IHomeRepository _homeRepository;

      

        public HomeController(IUserRepository userRepository, IHomeRepository homeRepository)
        {
            _userRepository = userRepository;
            _homeRepository = homeRepository;
        }
        public IActionResult Index()
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


            IEnumerable<Country> countries = _homeRepository.getCountries();
            HomeModel.countryList = countries;

            IEnumerable<City> cities = _homeRepository.getCities();
            HomeModel.cityList = cities;

            IEnumerable<MissionTheme> themes = _homeRepository.getThemes();
            HomeModel.themeList = themes;

            IEnumerable<Skill> skills = _homeRepository.getSkills();
            HomeModel.skillList = skills;

            
            IEnumerable<string> missiontitle = _homeRepository.GetMissionThemestitle();
            HomeModel.missiontitle=missiontitle;

            IEnumerable<string> missiondiscription = _homeRepository.GetMissionDiscription();
            HomeModel.missiondiscription = missiondiscription;



            return View(HomeModel);



        }

        public IActionResult GetCountries()
        {
            IEnumerable<Country> countries = _homeRepository.getCountries();
            return Json(new { data = countries });
        }
        public IActionResult GetCities()
        {
           IEnumerable<City> cities = _homeRepository.getCities();
          return Json(new { data = cities });
        }
        public IActionResult GetThemes()
        {
            IEnumerable<MissionTheme> missionThemes = _homeRepository.getThemes();
            return Json(new { data = missionThemes });
        }
        public IActionResult GetSkills()
        {
           IEnumerable<Skill> missionSkills = _homeRepository.getSkills();
            return Json(new { data = missionSkills });
        }

        public IActionResult GetMissionThemeTitle()
        {
            IEnumerable<string> missiontitle = _homeRepository.GetMissionThemestitle();
            return Json(new { data = missiontitle });   
        }
        public IActionResult GetMissionDiscription()
        {
            IEnumerable<string> missiondiscription = _homeRepository.GetMissionDiscription();
            return Json(new { data = missiondiscription });
        }
       public IActionResult GetMission()
        {
            IEnumerable<Mission> mission = _homeRepository.GetMissions();
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
            string gridview = System.Text.Json.JsonSerializer.Serialize(mission, options);
            return Json(new { data = gridview });

        }

        [HttpPost]
        public IActionResult gridSP(string country, string city, string theme, string skill, string searchText, string sorting, int pageNumber)
        {
            // make explicit SQL Parameter
            string userSession = HttpContext.Session.GetString("useremail");
            User userObj = _homeRepository.getuser(userSession);
            int uid = Convert.ToInt32(userObj.UserId);
            PaginationMission pagination = _homeRepository.gridSP(country, city, theme, skill, searchText, sorting, pageNumber, uid);

            return PartialView("_grid", pagination);
        }

        [HttpPost]
        public IActionResult addToFavourites(String missionid, int fav)
        {
            string userSession = HttpContext.Session.GetString("useremail");
            User userObj = _homeRepository.getuser(userSession);
            long misid = Int64.Parse(missionid);
            _homeRepository.addToFavourites(misid, userObj.UserId, fav);
            return RedirectToAction("Index");
        }




    }
}
