using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.ViewModels
{
    public class SessionDetailsViewModel
    {
        public string Email { get; set; }
        public string Avatar { get; set; }
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
    }
}
