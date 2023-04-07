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

        public long MissionId { get; set; }

        public TimeOnly? Time { get; set; }

        public TimeOnly? hours { get; set; }
        public TimeOnly? minutes { get; set; }  

        public int? Action { get; set; }

        public DateTime DateVolunteered { get; set; }

        public string? Notes { get; set; }

        public string Status { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}
