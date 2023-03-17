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
          MissionModel IMissionRepository.Getmission(string missionid)
        {
            //mission = _ciPlatformDbContext.MissionModel.FromSqlInterpolated($"exec sp_get_Mission_data @missionid = {missionid}");
            MissionModel mission = _ciPlatformDbContext.MissionModels.FromSqlInterpolated($"exec sp_get_Mission_data @missionid={missionid}").AsEnumerable().FirstOrDefault();
         
            return mission;

            
        }
        //List<ReleatedMissionModel> IMissionRepository.GetReleatedMission(string missiontitle)
        //{
        //    List<ReleatedMissionModel> mission1= _ciPlatformDbContext.ReleatedMissionModels.FromSqlInterpolated($"exec sp_get_reletedMission_data ").ToList();
        //    return mission1;
        //}
    }
}
