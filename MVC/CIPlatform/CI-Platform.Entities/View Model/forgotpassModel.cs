using System.ComponentModel.DataAnnotations;

namespace CI_Platform.Entities.ViewModel
{
    public class forgotpassModel
    {
        [Required]
        public string EmailId{ get; set;}
    }
}
