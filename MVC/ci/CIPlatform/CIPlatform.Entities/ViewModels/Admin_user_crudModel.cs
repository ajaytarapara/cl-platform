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
        public long? UserId { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "pls enter less than 10 char")]
        public string? FirstName { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "pls enter less than 20 char")]
        public string? LastName { get; set; }
        [Required]
        [RegularExpression("^[a-z]{1}[a-z0-9]+@[a-z]+\\.+[a-z]{2,3}$", ErrorMessage = "Please enter valid e-mail address")]
        public string? Email { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Please enter more than 8 characters")]
        public string? Password { get; set; }
        [Required]
        public long? PhoneNumber { get; set; }
        [Required]
        public string? EmplyoeeId { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "pls enter less than 10 char")]
        public string? Department { get; set; } 
        public string? profiletext { get; set; }
        public string? whyivol { get; set; }
        public string? Status { get; set; }
    }
}
