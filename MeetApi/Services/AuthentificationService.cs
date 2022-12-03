using System.Security.Claims;
using MeetApi.Interfaces.Services;
using Microsoft.AspNetCore.Authentication;
using MeetApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MeetApi.Models;
using Microsoft.AspNetCore.Authorization;
using User = MeetApi.Models.User;

namespace MeetApi.Services;

public class AuthentificationService : IAuthentificationService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly AppDbContext _db;

    public AuthentificationService(UserManager<User> userManager, SignInManager<User> signInManager, AppDbContext db)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _db = db;
    }

    public async Task LoginAsync(LoginToServer loginToServer)
    {
        var result =
            await _signInManager.PasswordSignInAsync(loginToServer.Login, loginToServer.Password,
                loginToServer.RememberMe, false);
        if (!result.Succeeded)
            throw new Exception("Wrong form");
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}