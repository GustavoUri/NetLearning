using MeetApi.Data;
using MeetApi.Models;
using Microsoft.AspNetCore.Identity;

namespace MeetApi.Interfaces.Services;

public interface IUserSearchService
{
    public List<UserToClient> GetUsersToClientByRecommendation(string userId);
    public List<User> GetUsersByFilter(Filter filter);
    public List<UserToClient> GetUsersToClientByFilter(Filter filter);
}