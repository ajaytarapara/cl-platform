using CIPlatform.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.ViewModels
{
    public class ShareStoryModel
    {
        public long? MissionId { get; set; }
        [Required]
        [MinLength(255, ErrorMessage = "Please enter more than 255 characters")]
        public string? Title { get; set; }
        [Required]
        [MinLength(2550, ErrorMessage = "Please enter more than 2550 characters")]
        public string? Description { get; set; }
        public DateTime? PublishedAt { get; set; }

        public string? username { get; set; }
        public string? avatar { get; set; }
        public long userid { get; set; }
        public IEnumerable<MissionApplication> getmission { get; set; }
        public string? StoryMedia { get; set; }

       public Story story { get; set; } 
    }
}
