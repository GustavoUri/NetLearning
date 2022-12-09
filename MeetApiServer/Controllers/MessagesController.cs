using Entities.Interfaces.Services;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace MeetApi.Controllers;

[ApiController]
public class MessagesController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IMessageSendingService _sendingService;
    private readonly IMessagesDataService _messagesDataService;

    public MessagesController(UserManager<User> userManager,
        IMessageSendingService sendingService, IMessagesDataService messagesDataService)
    {
        _userManager = userManager;
        _sendingService = sendingService;
        _messagesDataService = messagesDataService;
    }

    /// <summary>
    /// Sends message to other user
    /// </summary>
    /// <remarks>
    /// 
    ///     
    ///     
    ///  
    ///     POST /sendMessage
    ///     {
    ///         "receiverId": "03fee53b-e1a1-454f-bd67-85641ac2fc55",
    ///         "text": "Hello, Artyom!"
    ///      }
    ///    
    /// 
    /// </remarks>
    /// <response code="200">Successful</response>
    /// <response code="500">Server problem</response>
    /// <response code="400">Receiver not found</response>
    [HttpPost]
    [Authorize]
    [Route("sendMessage")]
    public async Task<IActionResult> SendMessage(MessageToServer messageToServer)
    {
        var sender = await _userManager.FindByNameAsync(User.Identity?.Name);
        await _sendingService.SendMessage(messageToServer, sender.Id);
        return Ok();
    }

    /// <summary>
    /// Returns all client messages with other user
    /// </summary>
    /// <remarks>
    /// 
    ///     
    ///     
    ///  
    ///     GET /getMessages
    ///     other user's id - "123eewe132esd"
    ///     
    ///    
    /// 
    /// </remarks>
    /// <response code="200">Successful</response>
    /// <response code="500">Server problem</response>
    /// <response code="400">Receiver not found</response>
    [HttpGet]
    [Authorize]
    [Route("getMessages")]
    public async Task<IActionResult> getMessageOfChat(string otherUserId)
    {
        var user = await _userManager.FindByNameAsync(User.Identity?.Name);
        var result = _messagesDataService.GetMessagesBetweenUsersForClient(user.Id, otherUserId);
        return Json(result);
    }
}