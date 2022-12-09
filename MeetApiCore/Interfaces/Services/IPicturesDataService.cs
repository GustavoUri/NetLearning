using Microsoft.AspNetCore.Http;

namespace Entities.Interfaces.Services;

public interface IPicturesDataService
{
    public Task AddPictureAsync(IFormFile picture, string path);
    public Task UpdateUserProfilePictureAsync(IFormFile picture, string userId);
    public Task<Byte[]> GetImageAsync(string path);
    public Task<Byte[]> GetUserProfilePictureAsync(string userId);
    
}