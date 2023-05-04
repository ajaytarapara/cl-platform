using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.ViewModels
{
    public  class NotificationModel
    {
        public int? userid { get; set; }
        public string? text { get; set; }
        public int? fromuserid { get; set; }
        public int? touserid { get; set; }
        public string? type { get; set; }
    }
}
