using CIPlatform.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.ViewModels
{
    public class Admin_user_crudModel
    {
        public string? adminname { get; set; }
        public string? adminavatar { get; set; }
        public Admin_NavbarModel? Navbar { get; set; }
        public List<User>? Users { get; set; }
        public long? UserId { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public long? PhoneNumber { get; set; }
        [Required]
        public string? EmplyoeeId { get; set; }
        [Required]
        public string? Department { get; set; } 

    }
}
