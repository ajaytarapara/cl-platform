using CIPlatform.Entities.ViewModels;
using CIPlatform.Models;
using CIPlatform.Repository.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CIPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IHomeRepository _homerepository;

        public HomeController(IUserRepository userRepository, IHomeRepository homeRepository  )
        {
            _userRepository = userRepository;
            _homerepository = homeRepository;
        }
        public IActionResult Index(HomeModel obj)
            {
                HomeModel HomeModel = new HomeModel();
                string email = "sagar@gmail.com";
                var finduser = _userRepository.findUser(email);

                HomeModel.username = finduser.FirstName + " " + finduser.LastName;
                var  cityname = _homerepository.getcity();
                return View(HomeModel);
            }


        public IActionResult Privacy()
        {
            return View();
        }

    }
}