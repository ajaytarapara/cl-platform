using CIPlatform.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.ViewModels
{
    public class VolunteeringTimesheetModel
    {
        public string? username { get; set; }
        public string? avatar { get; set; }
        public int? userid { get; set; }
       public IEnumerable<Timesheet>? timesheet { get; set; }
        public IEnumerable<Mission>? missiontitle { get; set; }
    }
}
