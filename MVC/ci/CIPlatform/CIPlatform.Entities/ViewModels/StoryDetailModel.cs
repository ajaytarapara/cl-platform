using CIPlatform.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.ViewModels
{
    public class StoryDetailModel
    {
        public string? navusername { get; set; }
        public string? navavatar { get; set; }
        public Story? story { get; set; }
        public List<User>? usersemails { get; set; }
        public string? searchemailtext { get; set; }
    }
}
