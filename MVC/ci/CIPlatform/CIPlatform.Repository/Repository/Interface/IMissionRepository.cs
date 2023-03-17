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
        public MissionModel Getmission(string missionid);

        //public List<ReleatedMissionModel> GetReleatedMission(string missiontitle);
    }
}
