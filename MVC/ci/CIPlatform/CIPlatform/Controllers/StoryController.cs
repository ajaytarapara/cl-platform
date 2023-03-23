using CIPlatform.Entities.DataModels;
using CIPlatform.Repository.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatform.Controllers
{
    public class StoryController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMissionRepository _missionRepository;
        private readonly IHomeRepository _homeRepository;
        private readonly CIPlatformDbContext _ciPlatformDbContext;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStoryRepository _storyRepository; 
        public StoryController(IUserRepository userRepository, IMissionRepository missionRepository, IHomeRepository homeRepository, CIPlatformDbContext cIPlatformDbContext, IConfiguration _configuration, IHttpContextAccessor httpContextAccessor,IStoryRepository storyRepository)
        {
            _missionRepository = missionRepository;
            _userRepository = userRepository;
            _homeRepository = homeRepository;
            _ciPlatformDbContext = cIPlatformDbContext;
            _httpContextAccessor = httpContextAccessor;
            configuration = _configuration;
        }
        public IActionResult StoryListing()
        {
            return View();
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
        //public IActionResult Storydata()
        //{
        //    Story story=_storyRepository.Storydata();   
        //    return PartialView("_grid", story);
        //}

    }
}
