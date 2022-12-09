using Entities.Models;

namespace Entities.Interfaces.Services;

public interface IUsersDataService
{
    public List<User> GetAllUsers();
    public User GetUserById(string id);
    public User GetUserByUserName(string userName);
}