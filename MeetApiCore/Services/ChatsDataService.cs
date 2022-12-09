using Entities.Data;
using Entities.Interfaces.Services;

namespace Entities.Services;

public class ChatsDataService : IChatsDataService
{
    private readonly IUsersDataService _usersDataService;
    private readonly AppDbContext _db;
    public ChatsDataService(IUsersDataService usersDataService, AppDbContext db)
    {
        _usersDataService = usersDataService;
        _db = db;

    }
    public List<string> GetAllFriendsIdsOfUser(string userId)
    {
        var user = _usersDataService.GetUserById(userId);
        var result = user.Friends.ToList();
        return result;
    }

    public void AddFriend(string userId, string friendId)
    {
        var user = _usersDataService.GetUserById(userId);
        var friend = _usersDataService.GetUserById(friendId);
        if(!user.Friends.Contains(friend.Id))
            user.Friends.Add(friend.Id);
        _db.SaveChanges();
    }
}