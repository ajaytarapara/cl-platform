using CIPlatform.Entities.ViewModels;
using CIPlatform.Repository.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using CIPlatform.Entities.DataModels;
using CIPlatform.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using CIPlatform.Helpers;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace CIPlatform.Controllers
{
    [Authorize(Roles="admin,volunteer")]
    public class MissionController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMissionRepository _missionRepository;
        private readonly IHomeRepository _homeRepository;
        private readonly CIPlatformDbContext _ciPlatformDbContext;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INotyfService _notyf;
        public MissionController(IUserRepository userRepository, IMissionRepository missionRepository, IHomeRepository homeRepository, CIPlatformDbContext cIPlatformDbContext, IConfiguration _configuration, IHttpContextAccessor httpContextAccessor, INotyfService notyf)
        {
            _missionRepository = missionRepository;
            _userRepository = userRepository;
            _homeRepository = homeRepository;
            _ciPlatformDbContext = cIPlatformDbContext;
            _httpContextAccessor = httpContextAccessor;
            configuration = _configuration;
            _notyf = notyf;
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
                HomeModel homeModel = new HomeModel();
                User userObj = _userRepository.findUser(userSessionEmailId);
                MissionModel missionModel = _missionRepository.Getmission(missionid, userObj.UserId);
                missionModel.username = userObj.FirstName + " " + userObj.LastName;
                homeModel.id = userObj.UserId;
                homeModel.avatar= userObj.Avatar;
                long UserId = userObj.UserId;
                long MissionId=missionModel.MissionId;
                missionModel.avatar = homeModel.avatar;
                return View(missionModel);
            }
        }

        public IActionResult GetReleatedMission(string missiontitle, string city, string country,long missionid)
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            if (userSessionEmailId == null)
            {
                return RedirectToAction("Login", "Account");
            }
                User userObj = _userRepository.findUser(userSessionEmailId);
                int userid = (int)userObj.UserId;
                MissionModel missionModel = new MissionModel();
            missionModel.Title = missiontitle;
            List<ReleatedMissionModel> releatedMissions = _missionRepository.GetReleatedMission(missiontitle, city, country,userid, missionid);
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
            return View("Mission_Volunteer");
        }

        [HttpPost]
        public void PostComments(String MissionID, String comment)
        {
            string userSession = HttpContext.Session.GetString("useremail");
            User userObj = _homeRepository.getuser(userSession);
            long misid = Int64.Parse(MissionID);
            int missid = Int32.Parse(MissionID);
            _missionRepository.addcomment(misid, userObj.UserId, comment);
            _notyf.Success("commented successfully", 3);
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
            string subject = "your friend recommanded to you for mission";
            MailHelper mailHelper = new MailHelper(configuration);
            MissionInvite missionInvite = new MissionInvite();
            missionInvite.FromUserId= userObj.UserId;
            long fromuserid=missionInvite.FromUserId;
            missionInvite.ToUserId= _missionRepository.GetInvitedUserid(cow_email, fromuserid, Missionid);
            missionInvite.MissionId = Missionid;
            missionInvite.CreatedAt = DateTime.Now;
            if (missionInvite.ToUserId != 0) {
                ViewBag.sendMail = mailHelper.Send(cow_email, welcomeMessage + path, subject);
                _missionRepository.AddinvitedMissionUser(missionInvite);
                _notyf.Success("mail sended successfully", 3);
            }
            else
            {
                _notyf.Error("emailid is not any user have");
            }

        }

        [HttpPost]
        public void ApplyApplication(long missionid)
      {
            string userSession = HttpContext.Session.GetString("useremail");
            User userObj = _homeRepository.getuser(userSession);
            _missionRepository.ApplyApplication(missionid,userObj.UserId);
            _notyf.Warning("your request submmited successfully", 3);
        }

        public IActionResult MissionUserRating(float ratingCount, long? missionid)
        {
           string userSession = HttpContext.Session.GetString("useremail");
            User user = _homeRepository.getuser(userSession);

            var findUserRating = _ciPlatformDbContext.MissionRatings.Where(x => x.UserId == user.UserId && x.MissionId == missionid).FirstOrDefault();

            if(findUserRating != null)
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

        public IActionResult GetRecentVolunteer(long missionid)
        {
            List<MissionApplication> recentvol=_missionRepository.GetRecentVolunteer(missionid);
            return PartialView("_RecentVolunteer", recentvol);
        }

        [HttpPost]
        public void addToFavouritesReletead(String missionid, int fav)
        {
            string userSession = HttpContext.Session.GetString("useremail");
            User userObj = _homeRepository.getuser(userSession);
            long misid = Int64.Parse(missionid);
            _homeRepository.addToFavourites(misid, userObj.UserId, fav);
        }
    }
}
