using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace MeetApi.Models;

public class User : IdentityUser
{
    [Key]
    private int age { get; set; }
    // [Key]
    // public int id { get; set; }
    // //public string email { get; set; }
    // //public string password { get; set; }
    // public string fullName { get; set; }
    // // public int age { get; set; }
    // // public string location { get; set; }
    // // public List<string> socialNetworks { get; set; }
    // // public string specialization { get; set; }
    // // public string gender { get; set; }
    // // public List<string> hobbies { get; set; }
    // // public string info { get; set; }
    // // public string genderPreferences { get; set; }
    // // public int[] prefAgeDiapazone { get; set; }
    // // public string photoName { get; set; }
    // //public string chatsId { get; set; }
    // public User()
    // {
    //     Messages = new HashSet<Message>();
    // }
    // public virtual ICollection<Message> Messages { get; set; }


}