using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.ViewModels
{
    public class StoryModel
    {
        [Key]
        public long? storyid { get; set; } 
        [Required]
        public string? storydescription { get; set; }
        [Required]
        public string? storytitle { get; set;}
        public string? userfirstname { get; set; }
        public string? userlastname { get; set; }
        public string? useravtar { get; set; }   
        public string? storymediapath { get; set; }

        public string? storymediatype { get; set; }  

        public string? themetitle { get; set; }

        public string? storystatus { get; set; }
        [Required]
        public DateTime? storypublishdate { get; set; }
    }
}
