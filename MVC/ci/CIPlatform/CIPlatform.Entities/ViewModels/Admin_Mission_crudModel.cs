using CIPlatform.Entities.DataModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.ViewModels
{
    public class Admin_Mission_crudModel
    {
        public string? adminname { get; set; }
        public string? adminavatar { get; set; }
        public List<Mission>? mission { get; set; }
        [Required]
        public string? missiontitle { get; set; }
        [Required]
        public string? shortdescription { get; set; }
        [Required]
        public string? longdescription { get; set; }
        [Required]
        public string? missiontype { get; set; }
        [Required]
        public DateTime? startdate { get; set; }
        [Required]
        public DateTime? enddate { get; set; }
        public int? goalvalue { get; set; }
        public string? goalobjective { get; set; }
        public int? country { get; set; }
        public string? countryname { get; set; }
        public int? city { get; set; }
        public string? cityname { get; set; }
        public int? totalseats { get; set; }
        public int? missiontheme { get; set; }
        [Required]
        public string? organizationname { get; set; }
        [Required]
        public string? organizationdescription { get; set; }
        public long? avaliblity { get; set; }
        public string? missionskills { get; set; }
        public long? missionid { get; set; }
        public string? filename { get; set; }
        public string? filetype { get; set; }
        public string? filepath { get; set; }
        public string? documentfile { get; set; }

    }
}
