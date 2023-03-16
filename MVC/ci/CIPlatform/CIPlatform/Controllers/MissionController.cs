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
        public IActionResult Mission_Volunteer(/*MissionModel obj*/)
        {
            // Mission mission = new Mission();
            //string missionid = null;
            //var missiondata=_missionRepository.Getmission(obj, missionid);
            //mission.MissionId = obj.MissionId;
            //mission.ThemeId = obj.ThemeId;
            //MissionMedium missionMedium = new MissionMedium();  
            //missionMedium.MediaName = obj.MediaName;
            //missionMedium.MediaPath = obj.MediaPath;
            //missionMedium.MediaType = obj.MediaType;

            return View(/*missiondata*/);
        }
    }
}
