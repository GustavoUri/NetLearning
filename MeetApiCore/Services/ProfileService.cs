using Entities.Data;
using Entities.Interfaces.Services;
using Entities.Models;

namespace Entities.Services;

public class ProfileService : IProfileService
{
    private readonly AppDbContext _db;
    private readonly IUsersDataService _usersDataService;
    private readonly ICitiesDataService _citiesDataService;
    private readonly IHobbiesDataService _hobbiesDataService;

    public ProfileService( AppDbContext db, 
        IUsersDataService usersDataService, ICitiesDataService citiesDataService, IHobbiesDataService hobbiesDataService)
    {
        _db = db;
        _usersDataService = usersDataService;
        _citiesDataService = citiesDataService;
        _hobbiesDataService = hobbiesDataService;
    }

    public void UpdateProfile(string userId, ProfileFormToServer profileFormToServer)
    {
       
            var user = _usersDataService.GetUserById(userId);
            if (profileFormToServer.Age != 0)
                user.Age = profileFormToServer.Age;
            if (profileFormToServer.FullName != null)
                user.FullName = profileFormToServer.FullName;
            if (profileFormToServer.Location != null)
                user.Location = _citiesDataService.GetAllCities().FirstOrDefault(x => x.Name == profileFormToServer.Location);
            if (profileFormToServer.Gender != null && (profileFormToServer.Gender == "M" || profileFormToServer.Gender == "F"))
                user.Gender = profileFormToServer.Gender;
            if (profileFormToServer.Hobbies != null)
                foreach (var hobby in profileFormToServer.Hobbies)
                {
                    var hobbyToUser = _hobbiesDataService.GetAllHobbies().FirstOrDefault(x => x.Name == hobby);
                    if (hobbyToUser != null)
                        user.Hobbies.Add(hobbyToUser);
                }

            user.Info = profileFormToServer.Info;
            _db.SaveChanges();
    }

    public Profile GetProfile(string userId)
    {
       
            var user = _usersDataService.GetUserById(userId);
            var userToClient = new Profile()
            {
                Id = user.Id,
                FullName = user.FullName,
                Gender = user.Gender,
                Age = user.Age,
                Hobbies = user.Hobbies.Select(hobby => hobby.Name).ToList(),
                Info = user.Info
            };
            if (user.Location != null)
                userToClient.Location = user.Location.Name;
            return userToClient;
    }
}