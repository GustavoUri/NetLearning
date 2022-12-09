using Entities.Data;
using Entities.Exceptions;
using Entities.Interfaces.Services;

namespace Entities.Services;

public class BlockedUsersDataService : IBlockedUsersDataService
{
    private readonly IUsersDataService _usersDataService;
    private readonly AppDbContext _db;

    public BlockedUsersDataService(IUsersDataService usersDataService, AppDbContext db)
    {
        _usersDataService = usersDataService;
        _db = db;
    }

    public List<string> GetBlockedUsersIdOfUser(string userId)
    {
        var user = _usersDataService.GetUserById(userId);
        var result = user.BlockedUsers.Select(blockedUser => blockedUser.Id).ToList();
        return result;
    }

    public void BlockUser(string userId, string idForBlocking)
    {
        var userForBlocking = _usersDataService.GetUserById(idForBlocking);
        var user = _usersDataService.GetUserById(userId);
        if (user.BlockedUsers.Contains(userForBlocking))
            throw new BadRequestException("User is has already been blocked");
        user.BlockedUsers.Add(userForBlocking);
        _db.SaveChanges();
    }

    public void UnblockUser(string userId, string idForUnblocking)
    {
        var userForUnBlocking = _usersDataService.GetUserById(idForUnblocking);
        var user = _usersDataService.GetUserById(userId);
        if (!user.BlockedUsers.Contains(userForUnBlocking))
            throw new BadRequestException("User was not blocked");
        user.BlockedUsers.Remove(userForUnBlocking);
        _db.SaveChanges();
    }
}