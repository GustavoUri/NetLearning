using Entities.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace Entities.Services;

public class PicturesDataService : IPicturesDataService
{
    private readonly IUsersDataService _usersDataService;

    public PicturesDataService(IUsersDataService usersDataService)
    {
        _usersDataService = usersDataService;
    }
    public async Task AddPictureAsync(IFormFile picture, string path)
    {
        await using var stream = new FileStream(path, FileMode.Create);
        await picture.CopyToAsync(stream);
    }

    public async Task UpdateUserProfilePictureAsync(IFormFile picture, string userId)
    {
        var path = $"Pictures/Avatars/{userId}";
        await AddPictureAsync(picture, path);
    }

    public async Task<byte[]> GetImageAsync(string path)
    {
        var picture = await File.ReadAllBytesAsync(path);
        return picture;
    }

    public async Task<byte[]> GetUserProfilePictureAsync(string userId)
    {
        var user = _usersDataService.GetUserById(userId);
        var path = $"Pictures/Avatars/{user.Id}";
        if (!File.Exists(path))
            path = $"Pictures/Avatars/ClassicPhoto/classicPhoto.jpg";
        var result = await GetImageAsync(path);
        return result;
        
    }
}