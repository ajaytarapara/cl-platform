using CIPlatform.Entities.ViewModels;
using CIPlatform.Repository.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using CIPlatform.Entities.DataModels;
using CIPlatform.Repository.Repository;

namespace CIPlatform.Controllers
{
    public class MissionController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMissionRepository _missionRepository;
        private readonly IHomeRepository _homeRepository;

        public MissionController(IUserRepository userRepository, IMissionRepository missionRepository,IHomeRepository homeRepository)
        {
            _missionRepository = missionRepository;
            _userRepository = userRepository;
            _homeRepository = homeRepository;
        }
        public IActionResult Mission_Volunteer(string missionid)
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");

            if (userSessionEmailId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                User userObj = _userRepository.findUser(userSessionEmailId);

                MissionModel missionModel = _missionRepository.Getmission(missionid,userObj.UserId);
                missionModel.username = userObj.FirstName + " " + userObj.LastName;
             

                return View(missionModel);
            }
        }

        public IActionResult GetReleatedMission(string missiontitle, string city, string country)
        {
            MissionModel missionModel = new MissionModel();
            missionModel.Title = missiontitle;
            List<ReleatedMissionModel> releatedMissions =_missionRepository.GetReleatedMission(missiontitle,city,country);
            return PartialView("_ReleatedMission",releatedMissions);
        }
        [HttpPost]
        public IActionResult addToFavourites(String missionid, int fav)
        {
            string userSession = HttpContext.Session.GetString("useremail");
            User userObj = _homeRepository.getuser(userSession);
            long misid = Int64.Parse(missionid);
            _missionRepository.addToFavourites(misid, userObj.UserId, fav);
            return RedirectToAction("Index");
        }
    }
}
