using System.ComponentModel.DataAnnotations;
using MeetApi.Exceptions;
using MeetApi.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using MeetApi.Models;
using User = MeetApi.Models.User;
using ValidationException = MeetApi.Exceptions.ValidationException;
namespace MeetApi.Services;

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
            throw new ValidationException(result.Errors.Select(error => error.Description).First());
        await _signInManager.SignInAsync(user, false);
    }
}