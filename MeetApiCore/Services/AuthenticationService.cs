using Entities.Exceptions;
using Entities.Interfaces.Services;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using User = Entities.Models.User;

namespace Entities.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly SignInManager<User> _signInManager;

    public AuthenticationService(SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task LoginAsync(LoginToServer loginToServer)
    {
        var result =
            await _signInManager.PasswordSignInAsync(loginToServer.Login, loginToServer.Password,
                loginToServer.RememberMe, false);
        if (!result.Succeeded)
            throw new BadRequestException("Wrong login or password");
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}