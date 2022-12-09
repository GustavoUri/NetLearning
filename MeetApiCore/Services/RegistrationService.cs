using Entities.Exceptions;
using Entities.Interfaces.Services;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using User = Entities.Models.User;

namespace Entities.Services;

public class RegistrationService : IRegistrationService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public RegistrationService(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task RegisterAsync(Register register)
    {
        var user = new User {UserName = register.Login};
        var result = await _userManager.CreateAsync(user, register.Password);
        if (!result.Succeeded)
            throw new BadRequestException(result.Errors.Select(error => error.Description).First());
        await _signInManager.SignInAsync(user, false);
    }
}