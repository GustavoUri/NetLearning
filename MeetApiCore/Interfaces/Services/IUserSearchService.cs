using Entities.Models;

namespace Entities.Interfaces.Services;

public interface IUserSearchService
{
    public List<string> GetUsersIdByRecommendation(string userId);
    public List<string> GetUsersIdByFilter(Filter filter, string userId);
}