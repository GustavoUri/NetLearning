using MeetApi.Data;
using MeetApi.Interfaces.Services;
using MeetApi.Models;
using Microsoft.AspNetCore.Identity;

namespace MeetApi.Services;

public class ProfileService : IProfileService
{
    private readonly UserManager<User> _userManager;
     private readonly SignInManager<User> _signInManager;
     private readonly AppDbContext _db;
     private readonly IDataService _dataService;

     public ProfileService(UserManager<User> userManager, SignInManager<User> signInManager, AppDbContext db, IDataService dataService)
     {
         _userManager = userManager;
         _signInManager = signInManager;
         _db = db;
         _dataService = dataService;
     }
    public void UpdateProfile(string userId, ProfileFormToServer profileFormToServer)
    {
        var user = _dataService.GetUserById(userId);
        user.Age = profileFormToServer.Age;
        user.FullName = profileFormToServer.FullName;
        user.Location = _db.Cities.FirstOrDefault(x => x.name == profileFormToServer.Location);
        user.Gender = profileFormToServer.Gender;
        if(profileFormToServer.Hobbies != null)
            foreach (var hobby in profileFormToServer.Hobbies)
            {
                var hobbyToUser = _dataService.GetAllHobbies().FirstOrDefault(x => x.Name == hobby);
                if(hobbyToUser != null) 
                    user.Hobbies.Add(hobbyToUser);
            }

        user.Info = profileFormToServer.Info;
        _dataService.SaveChanges();
    }
}