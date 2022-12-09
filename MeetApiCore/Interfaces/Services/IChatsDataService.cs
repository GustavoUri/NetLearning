namespace Entities.Interfaces.Services;

public interface IChatsDataService
{
    public List<string> GetAllFriendsIdsOfUser(string userId);
    public void AddFriend(string userId, string friendId);
}