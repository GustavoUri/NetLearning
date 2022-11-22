using System.ComponentModel.DataAnnotations;
 
namespace MeetApi.Models
{
    public class UserLogin
    {
        [Required]
        public string Login { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        public bool RememberMe { get; set; }
    }
}