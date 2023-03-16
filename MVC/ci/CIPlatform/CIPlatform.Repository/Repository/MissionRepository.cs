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
        //MissionModel IMissionRepository.Getmission(MissionModel obj,string missionid)
        //{
        //    return (MissionModel)_ciPlatformDbContext.MissionModel.FromSqlInterpolated($"exec sp_get_Mission_data @missionid = {missionid}");
        //}

    }
}