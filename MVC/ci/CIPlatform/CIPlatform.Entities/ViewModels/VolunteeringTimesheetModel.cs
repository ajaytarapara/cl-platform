using CIPlatform.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public TimeOnly? Time { get; set; }
        [Required]
        [MaxLength(2,ErrorMessage ="please enter valid hours")]
        public TimeOnly? hours { get; set; }
        [Required]
        [MaxLength(2, ErrorMessage = "please enter valid minutes")]
        public TimeOnly? minutes { get; set; }  
        [Required]
        public int? Action { get; set; }
        [Required]
        public DateTime? DateVolunteered { get; set; }
        [Required]
        public string? Notes { get; set; }

        public string Status { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}
