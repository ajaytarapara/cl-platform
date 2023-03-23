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
          MissionModel IMissionRepository.Getmission(string missionid,long userid)
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
                _ciPlatformDbContext.SaveChangesAsync();

            }
            else
            {
                FavouriteMission favouriteMission = _ciPlatformDbContext.FavouriteMissions.FirstOrDefault(x => x.MissionId == missionid && x.UserId == userid);
                _ciPlatformDbContext.FavouriteMissions.Remove(favouriteMission);
                _ciPlatformDbContext.SaveChangesAsync();
            }
        }

        List<ReleatedMissionModel> IMissionRepository.GetReleatedMission(string missiontitle, string city, string country)
        {
            List<ReleatedMissionModel> mission1 = _ciPlatformDbContext.ReleatedMissions.FromSqlInterpolated($"exec sp_get_reletedMission_data @missionreleated={missiontitle}, @cityreleated={city}, @countryreleated ={country}").ToList();
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
            _ciPlatformDbContext.SaveChangesAsync();
        }

        public void ApplyApplication(long missionid, long UserId)
        {
            MissionApplication application = new MissionApplication();
            application.UserId = UserId;
            application.MissionId = missionid;
            application.AppliedAt = DateTime.Now;
            application.ApprovalStatus = "Pending";

            bool isalreadyapplied = _ciPlatformDbContext.MissionApplications.Any(a => a.UserId == application.UserId);

            if (isalreadyapplied)
            {
                MissionApplication missionApplication=_ciPlatformDbContext.MissionApplications.Where(a => a.UserId == UserId).First();
                missionApplication.UserId = UserId;
                missionApplication.MissionId = missionid;
                missionApplication.AppliedAt = DateTime.Now;
                missionApplication.ApprovalStatus = "Pending";
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
        //public void GetRecentVolunteer(long missionid)
        //{
        //    _ciPlatformDbContext.MissionApplications.Include(A => A.User).Where(m => m.UserId.Equals(m.UserId && m.ApprovalStatus="applied"));
        //}
    }
}
