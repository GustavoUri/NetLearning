

using MeetApi.Models;

namespace MeetApi.Interfaces.Services;

public interface IRegistrationService
{
    Task RegisterAsync(Register register);
}