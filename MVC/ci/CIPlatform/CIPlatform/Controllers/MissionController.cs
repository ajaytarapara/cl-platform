using CIPlatform.Entities.ViewModels;
using CIPlatform.Repository.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using CIPlatform.Entities.DataModels;
using CIPlatform.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using CIPlatform.Helpers;

namespace CIPlatform.Controllers
{
    public class MissionController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMissionRepository _missionRepository;
        private readonly IHomeRepository _homeRepository;
        private readonly CIPlatformDbContext _ciPlatformDbContext;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MissionController(IUserRepository userRepository, IMissionRepository missionRepository, IHomeRepository homeRepository, CIPlatformDbContext cIPlatformDbContext, IConfiguration _configuration, IHttpContextAccessor httpContextAccessor)
        {
            _missionRepository = missionRepository;
            _userRepository = userRepository;
            _homeRepository = homeRepository;
            _ciPlatformDbContext = cIPlatformDbContext;
            _httpContextAccessor = httpContextAccessor;
            configuration = _configuration;
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

                MissionModel missionModel = _missionRepository.Getmission(missionid, userObj.UserId);
                missionModel.username = userObj.FirstName + " " + userObj.LastName;

                return View(missionModel);
            }
        }

        public IActionResult GetReleatedMission(string missiontitle, string city, string country)
        {
            MissionModel missionModel = new MissionModel();
            missionModel.Title = missiontitle;
            List<ReleatedMissionModel> releatedMissions = _missionRepository.GetReleatedMission(missiontitle, city, country);
            return PartialView("_ReleatedMission", releatedMissions);
        }
        [HttpPost]
        public void addToFavourites(long missionid, int fav)
        {
            string userSession = HttpContext.Session.GetString("useremail");
            User userObj = _homeRepository.getuser(userSession);

            _missionRepository.addToFavourites(missionid, userObj.UserId, fav);
        }

        public IActionResult addcomment()
        {
            return View(Mission_Volunteer);
        }

        [HttpPost]
        public void PostComments(String MissionID, String comment)
        {
            string userSession = HttpContext.Session.GetString("useremail");
            User userObj = _homeRepository.getuser(userSession);
            long misid = Int64.Parse(MissionID);
            int missid = Int32.Parse(MissionID);
            _missionRepository.addcomment(misid, userObj.UserId, comment);
            //List<Comment> comment1 = _context.Comments.Where(x => x.MissionId == missid).AsEnumerable().ToList();

        }
        public IActionResult ListComments(int MissionID)
        {
            string userSession = HttpContext.Session.GetString("useremail");
            User userObj = _homeRepository.getuser(userSession);
            //_missionListingRepository.addComments(misid, userObj.UserId, comment);
            List<Comment> comment1 = _ciPlatformDbContext.Comments.Where(x => x.MissionId == MissionID).Include(x => x.User).AsEnumerable().ToList();
            return PartialView("_CommentMission", comment1);
        }

        [HttpPost]
        public void recommendedtocoworker(string cow_email, int Missionid)
        {
            string userSession = HttpContext.Session.GetString("useremail");
            User userObj = _homeRepository.getuser(userSession);
            string welcomeMessage = "Welcome to CI platform, <br/> You can participate in mission using below link. </br>";
            string path = "<a href=\"" + " https://" + _httpContextAccessor.HttpContext.Request.Host.Value + "/Mission/Mission_Volunteer?id=" + Missionid.ToString() + " \"  style=\"font-weight:500;color:blue;\" > Apply to Mission </a>";
            MailHelper mailHelper = new MailHelper(configuration);
            ViewBag.sendMail = mailHelper.Send(cow_email, welcomeMessage + path);

        }

        [HttpPost]
        public void ApplyApplication(long missionid)
      {
            string userSession = HttpContext.Session.GetString("useremail");
            User userObj = _homeRepository.getuser(userSession);
            _missionRepository.ApplyApplication(missionid,userObj.UserId);
        }

        public IActionResult MissionUserRating(float ratingCount, long? missionid)
        {
           string userSession = HttpContext.Session.GetString("useremail");
            User user = _homeRepository.getuser(userSession);

            var findUserRating = _ciPlatformDbContext.MissionRatings.Where(x => x.UserId == user.UserId && x.MissionId == missionid).FirstOrDefault();

            if (findUserRating != null)
            {
                findUserRating.UserId = user.UserId;
                findUserRating.MissionId = (long)missionid;
                findUserRating.Rating = (byte)ratingCount;
                findUserRating.UpdatedAt = DateTime.Now;
                _ciPlatformDbContext.MissionRatings.Update(findUserRating);
                _ciPlatformDbContext.SaveChanges();
            }
            else
            {
                MissionRating missionRating = new MissionRating();
                missionRating.UserId = user.UserId;
                missionRating.MissionId = (long)missionid;
                missionRating.Rating = (byte)ratingCount;
                var entry = _ciPlatformDbContext.MissionRatings.Add(missionRating);
                _ciPlatformDbContext.SaveChanges();

            }
            return Ok();
        }
    }
}
