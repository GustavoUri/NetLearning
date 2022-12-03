using MeetApi.Data;
using MeetApi.Interfaces.Services;
using MeetApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MeetApi.Services;

public class UserSearchService : IUserSearchService
{
    private readonly UserManager<User> _userManager;
    private readonly AppDbContext _db;
    private readonly IDataService _dataService;

    public UserSearchService(UserManager<User> userManager, AppDbContext db, IDataService dataService)
    {
        _userManager = userManager;
        _db = db;
        _dataService = dataService;
    }


    public List<UserToClient> GetUsersToClientByRecommendation(string userId)
    {
        var mainUser = _dataService.GetUserById(userId);
        var coincidenceToUser = new Dictionary<UserToClient, int>();
        var users = _dataService.GetAllUsers();
        foreach (var user in users)
        {
            var count = 0;
            if (user.Location == mainUser.Location)
                count++;
            if (user.Gender == mainUser.Gender)
                count++;
            count += mainUser.Hobbies.Count(user.Hobbies.Contains);
            coincidenceToUser.Add(new UserToClient(user), count);
        }

        var result = coincidenceToUser
            .OrderByDescending(x => x.Value)
            .Select(x => x.Key)
            .ToList();
        return result;
    }


    public List<User> GetUsersByFilter(Filter filter)
    {
        var result = _dataService.GetAllUsers();
        if (filter.Gender != null)
        {
            result = result.Where(x => x.Gender == filter.Gender).ToList();
        }

        result = result.Where(x => x.Age >= filter.FirstAgeBorder && x.Age <= filter.SecondAgeBorder).ToList();

        if (filter.Hobbies != null)
        {
            result = result
                .Where(user => user.Hobbies
                    .Select(hobby => hobby.Name)
                    .OrderBy(hobby => hobby)
                    .SequenceEqual(filter.Hobbies.OrderBy(hobby => hobby)))
                .ToList();
        }

        if (filter.Locations != null)
        {
            result = result
                .Where(user => user.Location != null && filter.Locations.Contains(user.Location.name))
                .ToList();
        }


        return result;
    }

    public List<UserToClient> GetUsersToClientByFilter(Filter filter)
    {
        var users = GetUsersByFilter(filter).Select(user => new UserToClient(user)).ToList();

        return users;
    }
}