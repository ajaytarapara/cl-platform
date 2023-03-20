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
        void IMissionRepository.addcomment(Comment comment)
        {
            _ciPlatformDbContext.Comments.Add(comment);
            _ciPlatformDbContext.SaveChanges();
        }
    }
}
