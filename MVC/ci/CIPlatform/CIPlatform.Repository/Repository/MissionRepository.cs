using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Repository
{
    public class MissionRepository : IMissionRepository
    {
        private readonly CIPlatformDbContext _ciPlatformDbContext;

        public MissionRepository(CIPlatformDbContext cIPlatformDbContext)
        {
            _ciPlatformDbContext = cIPlatformDbContext;
        }
        MissionModel IMissionRepository.Getmission(string missionid, long userid)
        {
            //mission = _ciPlatformDbContext.MissionModel.FromSqlInterpolated($"exec sp_get_Mission_data @missionid = {missionid}");
            MissionModel mission = _ciPlatformDbContext.MissionModels.FromSqlInterpolated($"exec sp_get_Mission_data @missionid={missionid},@UserId={userid}").AsEnumerable().FirstOrDefault();

            return mission;

        }

        public void addToFavourites(long missionid, long userid, int fav)
        {
            if (fav == 0)
            {
                FavouriteMission favouriteMission = new FavouriteMission();
                favouriteMission.MissionId = missionid;
                favouriteMission.UserId = userid;
                favouriteMission.CreatedAt = DateTime.Now;
                _ciPlatformDbContext.FavouriteMissions.Add(favouriteMission);
                _ciPlatformDbContext.SaveChanges();

            }
            else
            {
                FavouriteMission favouriteMissions = _ciPlatformDbContext.FavouriteMissions.FirstOrDefault(x => x.MissionId == missionid && x.UserId == userid);
   
                _ciPlatformDbContext.FavouriteMissions.Remove(favouriteMissions);
                _ciPlatformDbContext.SaveChanges();
    
            }
        }

        List<ReleatedMissionModel> IMissionRepository.GetReleatedMission(string missiontitle, string city, string country,int userid,long missionid)
        {
            List<ReleatedMissionModel> mission1 = _ciPlatformDbContext.ReleatedMissions.FromSqlInterpolated($"exec sp_get_releatedData @missionreleated={missiontitle}, @cityreleated={city}, @countryreleated ={country},@UserId={userid},@Missionid={missionid}").ToList();
            return mission1;
        }
       public void addcomment(long missionid, long userid ,string Commenttext)
        {
            Comment comment = new Comment();
            comment.MissionId = missionid;
            comment.UserId = userid;
            comment.CreatedAt = DateTime.Now;
            comment.Comment1 = Commenttext;
            _ciPlatformDbContext.Comments.Add(comment);
            _ciPlatformDbContext.SaveChanges();
        }

        public void ApplyApplication(long missionid, long UserId)
        {
            MissionApplication application = new MissionApplication();
            application.UserId = UserId;
            application.MissionId = missionid;
            application.AppliedAt = DateTime.Now;
            application.ApprovalStatus = "pending";

            bool isalreadyapplied = _ciPlatformDbContext.MissionApplications.Any(a => a.UserId == application.UserId);

            if (isalreadyapplied)
            {
                MissionApplication missionApplication=_ciPlatformDbContext.MissionApplications.Where(a => a.UserId == UserId).First();
                missionApplication.UserId = UserId;
                missionApplication.MissionId = missionid;
                missionApplication.AppliedAt = DateTime.Now;
                missionApplication.ApprovalStatus = "pending";
                _ciPlatformDbContext.Update(missionApplication);

            }
            else
            {
                _ciPlatformDbContext.Add(application);
            }
            _ciPlatformDbContext.SaveChanges();
        }

        void IMissionRepository.addRatingStars(int userId, int missionId, int ratingStars)
        {
            MissionRating missionRating = new MissionRating();
            missionRating.UserId = userId;
            missionRating.MissionId = missionId;
            missionRating.Rating = (byte)ratingStars;
            bool hasAlreadyRating = _ciPlatformDbContext.MissionRatings.Any(u => u.UserId == userId && u.MissionId == missionId);
            if (hasAlreadyRating)
            {
                MissionRating missionRatingObj = _ciPlatformDbContext.MissionRatings.Where(r => r.UserId == userId && r.MissionId == missionId).First();
                missionRatingObj.Rating = (byte)ratingStars;
                _ciPlatformDbContext.MissionRatings.Update(missionRatingObj);
                _ciPlatformDbContext.SaveChanges();


            }
            else
            {
                _ciPlatformDbContext.MissionRatings.Add(missionRating);
                _ciPlatformDbContext.SaveChanges();
            }
        }
        public List<MissionApplication> GetRecentVolunteer(long missionid)
        {
            List<MissionApplication>recentvol= _ciPlatformDbContext.MissionApplications.Where(m => m.MissionId==missionid).Include(A => A.User).ToList();
            return recentvol;
        }
        MissionApplication IMissionRepository.GetAppliedBtnForUser(long UserId, long MissionId)
        {
           MissionApplication missionApplicationstatus = _ciPlatformDbContext.MissionApplications.Include(x => x.User).Where(x => x.UserId == UserId && x.MissionId == MissionId).FirstOrDefault();
            return missionApplicationstatus;
        }
        long IMissionRepository.GetInvitedUserid(string cow_email, long fromuserid, long missionId)
        {
            User user = _ciPlatformDbContext.Users.Where(x => x.Email == cow_email).FirstOrDefault();
            if(user != null)
            {
                Notification notification = new Notification();
                notification.NotificationType = "recommanded from mission";
                notification.ToUserId = (int?)fromuserid;
                notification.ToUserId = (int?)user.UserId;
                notification.CreatedAt = DateTime.Now;
                notification.Status = "notseen";
                notification.NotificationText = "<a href='/Mission/Mission_Volunteer?missionId=" + missionId + "'/>" + " you can see mission " + "</a>";
                _ciPlatformDbContext.Add(notification);
                _ciPlatformDbContext.SaveChanges();
                long userid = user.UserId;
                return userid;
            }
            else
            {
                return 0;
            }
      
        }
        void IMissionRepository.AddinvitedMissionUser(MissionInvite missionInvite)
        {
            _ciPlatformDbContext.Add(missionInvite);
            _ciPlatformDbContext.SaveChanges();
        }
    }
}
