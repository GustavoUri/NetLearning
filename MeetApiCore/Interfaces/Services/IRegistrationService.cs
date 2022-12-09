

using Entities.Models;

namespace Entities.Interfaces.Services;

public interface IRegistrationService
{
    Task RegisterAsync(Register register);
}