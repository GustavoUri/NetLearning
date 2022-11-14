using System.ComponentModel.DataAnnotations;

namespace MeetApi.Models;

public class RegisterModel
{
    
        [Required (ErrorMessage = "Не указан логин")] 
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина строки логина должна быть от 3 до 20 символов")]
        public string Login { get; set; }
        
 
        [Required (ErrorMessage = "Не указан пароль")]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Длина строки пароля должна быть от 5 до 25 символов")]
        public string Password { get; set; }
 
        [Required(ErrorMessage = "Не указано подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }
    
}