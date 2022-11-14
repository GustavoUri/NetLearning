using System.ComponentModel.DataAnnotations;

namespace MeetApi.Models;

public class RegisterModel
{
    
        [Required (ErrorMessage = "Не указан логин")] 
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина строки логина должна быть от 3 до 50 символов")]
        public string Login { get; set; }
        
 
        [Required]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Длина строки пароля должна быть от 3 до 50 символов")]
        public string Password { get; set; }
 
        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }
    
}