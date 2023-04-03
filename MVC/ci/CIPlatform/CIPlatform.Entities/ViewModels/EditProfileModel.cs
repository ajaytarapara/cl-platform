using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.ViewModels
{
    public class EditProfileModel
    {
        [Key]
        public long userid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string avatar { get; set; }
        public string department { get; set; }  

        public string profiletext { get; set; }

        public string whyivol { get; set; }


    }
}
