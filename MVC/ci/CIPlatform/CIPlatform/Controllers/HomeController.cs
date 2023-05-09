
using AspNetCoreHero.ToastNotification.Abstractions;
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

        private readonly INotyfService _notyf;

        public HomeController(IUserRepository userRepository, IHomeRepository homeRepository, INotyfService notyf)
        {
            _userRepository = userRepository;
            _homeRepository = homeRepository;
            _notyf = notyf;
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
            HomeModel.avatar = userObj.Avatar;
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
            HomeModel.missiontitle = missiontitle;

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
        public IActionResult gridSP(string country, string city, string theme, string skill, string searchText, string sorting, int pageNumber,string explore)
        {
            // make explicit SQL Parameter
            string userSession = HttpContext.Session.GetString("useremail");
            User userObj = _homeRepository.getuser(userSession);
            int uid = Convert.ToInt32(userObj.UserId);
            PaginationMission pagination = _homeRepository.gridSP(country, city, theme, skill, searchText, sorting, pageNumber, uid, explore);
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
        public IActionResult GetCitiesFromCountry(string country)
        {
            if (country != null)
            {
                string[] countrynames = country.Split(",").SkipLast(1).ToArray();
                IEnumerable<City> citylist = new List<City>();
                JsonSerializerOptions options = new()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    WriteIndented = true
                };
                foreach (string countryname in countrynames)
                {
                    List<City> tmplist = _homeRepository.GetCityFromCountry(countryname);
                    citylist = citylist.Concat(tmplist.AsEnumerable());
                }
                string gridview = System.Text.Json.JsonSerializer.Serialize(citylist, options);

                return Json(new { data = gridview });
            }
            else
            {
                IEnumerable<City> citylist = new List<City>();
                JsonSerializerOptions options = new()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    WriteIndented = true
                };
                List<City> tmplist = _homeRepository.GetCityFromCountry();
                citylist = citylist.Concat(tmplist.AsEnumerable());
                string gridview = System.Text.Json.JsonSerializer.Serialize(citylist, options);

                return Json(new { data = gridview });
            }
        }

        //==============================================================================
        //notification
        //===============================================================================
        public IActionResult GetNotification()
        {
            string userSession = HttpContext.Session.GetString("useremail");
            User userObj = _homeRepository.getuser(userSession);
            int userid = Convert.ToInt32(userObj.UserId);
            var notificationList = _homeRepository.GetNotificationforUser(userid).ToList();
            return Json(new { data = notificationList });
        }

        public IActionResult GetNotificationCount()
        {
            string userSession = HttpContext.Session.GetString("useremail");
            User userObj = _homeRepository.getuser(userSession);
            int userid = Convert.ToInt32(userObj.UserId);
            var notificationList = _homeRepository.GetNotificationforUserCount(userid).ToList();
            var notyficationcount = notificationList.Count();
            return Json(new { data = notyficationcount });
        }
        [HttpPost]
        public void clearnotification()
        {
            string userSession = HttpContext.Session.GetString("useremail");
            User userObj = _homeRepository.getuser(userSession);
            int userid = Convert.ToInt32(userObj.UserId);
            _homeRepository.ClearNotification(userid);
        }
        [HttpPost]
        public void changenotificationstatus(long notyid)
        {
            string userSession = HttpContext.Session.GetString("useremail");
            User userObj = _homeRepository.getuser(userSession);
            int userid = Convert.ToInt32(userObj.UserId);
            _homeRepository.UpdateNotificationStatus(notyid);
        }
    }
}
