using System.ComponentModel.DataAnnotations;

namespace CI_Platform.Entities.ViewModel
{
    public class LoginModel
    {    
        [Required]  
        public string EmailId { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
