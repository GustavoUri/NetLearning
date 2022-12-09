using Entities.Interfaces.Services;
using Entities.Models;

namespace Entities.Services;

public class UserSearchService : IUserSearchService
{
    private readonly IUsersDataService _usersDataService;

    public UserSearchService(IUsersDataService usersDataService)
    {
        _usersDataService = usersDataService;
    }


    public List<string> GetUsersIdByRecommendation(string userId)
    {
        var mainUser = _usersDataService.GetUserById(userId);
        var coincidenceToUser = new Dictionary<string, int>();
        var users = _usersDataService.GetAllUsers().Where(user => user != mainUser);
        foreach (var user in users)
        {
            var count = 0;
            if (user.Location == mainUser.Location)
                count++;
            if (user.Gender == mainUser.Gender)
                count++;
            count += mainUser.Hobbies.Count(user.Hobbies.Contains);
            coincidenceToUser.Add(user.Id, count);
        }

        var result = coincidenceToUser
            .OrderByDescending(x => x.Value)
            .Select(x => x.Key)
            .ToList();
        return result;
    }


    public List<string> GetUsersIdByFilter(Filter filter, string userId)
    {
        var filteredUsers = _usersDataService.GetAllUsers().Where(user => user.Id != userId);
        if (filter.Gender != null)
        {
            filteredUsers = filteredUsers.Where(x => x.Gender == filter.Gender);
        }

        filteredUsers = filteredUsers.Where(x => x.Age >= filter.FirstAgeBorder && x.Age <= filter.SecondAgeBorder);

        if (filter.Hobbies != null)
        {
            filteredUsers = filteredUsers
                .Where(user => user.Hobbies
                    .Select(hobby => hobby.Name)
                    .OrderBy(hobby => hobby)
                    .SequenceEqual(filter.Hobbies.OrderBy(hobby => hobby)));
        }

        if (filter.Locations != null)
        {
            filteredUsers = filteredUsers
                .Where(user => user.Location != null && filter.Locations.Contains(user.Location.Name));
        }

        var result = filteredUsers.Select(user => user.Id).ToList();


        return result;
    }
    
}