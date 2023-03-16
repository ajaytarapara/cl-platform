using CIPlatform.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.ViewModels
{
    public class HomeModel
    {
        public string username{ get; set; }

        public long id { get; set; }
        public IEnumerable<Country> countryList { get; set; }
        public IEnumerable<City> cityList { get; set; }
        public IEnumerable<MissionTheme> themeList { get; set; }
        public IEnumerable<Skill> skillList { get; set; }
        public IEnumerable<string> missiontitle { get; set; }

        public IEnumerable<string> missiondiscription { get; set; }

        public int  Missionid { get; set; }
        public IEnumerable<string> MissionTheme { get; set; }
        public string MissionTitle { get; set; }
        public string CityName{ get; set; }
        public string MissionShortDescription { get; set; }
        public string OrganizationName { get; set; }
        public string mediapath { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Avaliblity { get; set; }
        public string Rating { get; set; }
        public string Medianame { get; set; }
        public string Mediatype { get; set; }
        public string themetitle { get; set; }


    }
}
