namespace Entities.Interfaces.Services;

public interface IBlockedUsersDataService
{
    public List<string> GetBlockedUsersIdOfUser(string userId);
    public void BlockUser(string userId, string idForBlocking);
    public void UnblockUser(string userId, string idForUnblocking);
}