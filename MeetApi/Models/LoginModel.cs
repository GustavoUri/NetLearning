using System.ComponentModel.DataAnnotations;
 
namespace MeetApi.Models
{
    public class LoginModel
    {
        [Required (ErrorMessage = "Не указан логин")]
        public string Login { get; set; }
        
        [Required (ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }
        
        public bool RememberMe { get; set; }
    }
}