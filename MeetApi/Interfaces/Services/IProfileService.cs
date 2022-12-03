using MeetApi.Models;

namespace MeetApi.Interfaces.Services;

public interface IProfileService
{
    public void UpdateProfile(string userId, ProfileFormToServer profileFormToServer);

}