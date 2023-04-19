using CIPlatform.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.ViewModels
{
    public  class Admin_mission_theme_crudModel
    {
        public string? adminname { get; set; }
        public string? adminavatar { get; set; }
        public List<MissionTheme>? missionthemes { get; set; }
    }
}
