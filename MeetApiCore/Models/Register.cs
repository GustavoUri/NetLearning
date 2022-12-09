using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class Register 
{
    
        [Required (ErrorMessage = "No login")] 
        [StringLength(20, MinimumLength = 3, ErrorMessage = "The length of the login should be from 3 to 20 characters")]
        public string Login { get; set; }
        
 
        [Required (ErrorMessage = "No password")]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "The length of the password should be from 5 to 25 characters")]
        public string Password { get; set; }
        
    
}