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

        public IEnumerable<Country> countryList { get; set; }
        public IEnumerable<City> cityList { get; set; }
        public IEnumerable<MissionTheme> themeList { get; set; }
        public IEnumerable<Skill> skillList { get; set; }
        public IEnumerable<string> missiontitle { get; set; }

        public IEnumerable<string> missiondiscription { get; set; }
    }
}
