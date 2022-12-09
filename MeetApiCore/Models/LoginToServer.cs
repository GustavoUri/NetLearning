using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class LoginToServer  
    {
        [Required]
        public string Login { get; set; }
        
        [Required]
        public string Password { get; set; }
        [Required]
        public bool RememberMe { get; set; }
    }
}