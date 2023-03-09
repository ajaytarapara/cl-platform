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

        public IEnumerable<string>  Missionid { get; set; }
        public IEnumerable<string> MissionTheme { get; set; }
        public IEnumerable<string> MissionTitle { get; set; }
        public IEnumerable<string> CityName{ get; set; }
        public IEnumerable<string> MissionShortDescription { get; set; }
        public IEnumerable<string> OrganizationName { get; set; }
        public IEnumerable<string> Imagepath { get; set; }
        public IEnumerable<string> StartDate { get; set; }
        public IEnumerable<string> EndDate { get; set; }
        public IEnumerable<string> Avaliblity { get; set; }
        public IEnumerable<string> Rating { get; set; }

        public static object? FromSqlInterpolated(string v)
        {
            throw new NotImplementedException();
        }
    }
}
