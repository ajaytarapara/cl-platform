using System.ComponentModel.DataAnnotations;

namespace CIPlatform.Models
{
    public class RegisterModel
    {    
        [Required]
        public string Fname { get; set; }
        [Required]
        public string Lname { get; set; }
        [Required]
        public string Phonenumber { get; set; }
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }

    }
}
