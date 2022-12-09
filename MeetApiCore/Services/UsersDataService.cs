using Entities.Data;
using Entities.Exceptions;
using Entities.Interfaces.Services;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities.Services;

public class UsersDataService : IUsersDataService
{
    private readonly AppDbContext _db;

    public UsersDataService(AppDbContext db)
    {
        _db = db;
    }

    public List<User> GetAllUsers()
    {
        var result = _db.Users
            .Include(user => user.Hobbies)
            .Include(user => user.Location)
            .Include(user => user.BlockedUsers).ToList();
        return result;
    }

    public User GetUserById(string id)
    {
        var result = GetAllUsers().FirstOrDefault(user => user.Id == id);
        if (result == null)
            throw new BadRequestException($@"User with id {id} was not found");
        return result;
    }


    public User GetUserByUserName(string userName)
    {
        var result = GetAllUsers().FirstOrDefault(user => user.UserName == userName);
        if (result == null)
            throw new BadRequestException($@"User with userName {userName} was not found");
        return result;
    }
}