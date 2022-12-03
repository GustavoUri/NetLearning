namespace MeetApi.Interfaces.Services;

public interface IPictureService
{
    public void UpdatePicture(IFormFile? picture);
    public IFormFile GetImage(string pictureName);
}