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

        public MissionController(IUserRepository userRepository, IMissionRepository missionRepository)
        {
            _missionRepository = missionRepository;
            _userRepository = userRepository;
        }

       
    }
}
