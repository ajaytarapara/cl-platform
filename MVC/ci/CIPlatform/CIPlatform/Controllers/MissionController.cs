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
                MissionModel missionModel = _missionRepository.Getmission(missionid);
                missionModel.username = userObj.FirstName + " " + userObj.LastName;
                return View(missionModel);
            }
        }


    }
}
