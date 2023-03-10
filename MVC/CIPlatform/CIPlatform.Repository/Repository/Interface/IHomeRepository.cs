using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Repository.Interface
{
    public interface IHomeRepository
    {

        public IEnumerable<Country> getCountries();
        public IEnumerable<City> getCities();
        public IEnumerable<MissionTheme> getThemes();
        public IEnumerable<Skill> getSkills();

        public IEnumerable<Mission> GetMissions();

        public IEnumerable<string> Getimgmissionurl();

        public IEnumerable<string> GetMissioncity();
        public IEnumerable<string> GetMissionThemestitle();

        public IEnumerable<string> GetMissionDiscription();

        public IEnumerable<string> GetMissionstartdate();

        public IEnumerable<string> GetMissionenddate();
        public IEnumerable<GridModel> Getgridview();

    }
}
