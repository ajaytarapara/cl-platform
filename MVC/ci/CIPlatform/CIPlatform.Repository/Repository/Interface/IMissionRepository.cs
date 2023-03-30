using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Repository.Interface
{
    public interface IMissionRepository
    {
        public void addToFavourites(long misid, long UserId, int fav);

        public void addcomment(long userid, long missionid, string Commenttext);
        public void addRatingStars(int userId, int missionId, int ratingStars);
        public MissionModel Getmission(string missionid,long userid);

        public List<ReleatedMissionModel> GetReleatedMission(string missiontitle,string city,string country);
        public void ApplyApplication(long UserId, long missionid);
        public List<MissionApplication> GetRecentVolunteer(long missionid);
    }
}
