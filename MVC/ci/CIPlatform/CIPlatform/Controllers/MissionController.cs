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
        //public IActionResult Mission_Volunteer()
        //{
          
        //    MissionModel model = _missionRepository.getMISSION(MissionModel Obj);
        //    model.MediaPath = Obj.MediaPath;
        //    model.MediaName = Obj.MediaName;
        //    model.MediaType = Obj.MediaType;
        //    return View(model);
           

        //}
    }
}
