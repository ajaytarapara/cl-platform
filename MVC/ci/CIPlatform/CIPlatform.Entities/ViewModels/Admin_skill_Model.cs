using CIPlatform.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.ViewModels
{
    public class Admin_skill_Model
    {
        public string? adminname { get; set; }
        public string? adminavatar { get; set; }
        public List<Skill>? skills { get; set; }
        [Required]
        public string? SkillTitle { get; set; }
        public long? SkillId { get; set; }
    }
}
