using Entities.Models;

namespace Entities.Interfaces.Services;

public interface IProfileService
{
    public void UpdateProfile(string userId, ProfileFormToServer profileFormToServer);
    public Profile GetProfile(string userId);

}