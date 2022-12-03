
using MeetApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MeetApi.Models;
using Microsoft.AspNetCore.Authorization;
namespace MeetApi.Interfaces.Services;

public interface IAuthentificationService
{
    Task LoginAsync(LoginToServer loginToServer);
    Task LogoutAsync();

}