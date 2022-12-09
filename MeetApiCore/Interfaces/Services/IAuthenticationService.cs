using Entities.Models;

namespace Entities.Interfaces.Services;

public interface IAuthenticationService
{
    Task LoginAsync(LoginToServer loginToServer);
    Task LogoutAsync();
}