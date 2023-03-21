using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.ViewModels
{
    public class CommentModel
    {
        public long CommentId { get; set; }

        public long UserId { get; set; }

        public long MissionId { get; set; }
        public string commentext { get; set; }  
    }
}
