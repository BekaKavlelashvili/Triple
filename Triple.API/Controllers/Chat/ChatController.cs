using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Triple.Application.Commands.Chat;
using Triple.Application.Commands.Hubs;
using Triple.Application.Queries.Chat;

namespace Triple.API.Controllers.Chat
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ChatController : BaseController
    {
        public ChatController(IMediator mediator) : base(mediator)
        {

        }

        [HttpPost]
        //[AuthPermission("CreateChat")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateChatCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }

        [HttpPost("message")]
        //[AuthPermission("AddMessage")]
        public async Task<IActionResult> AddMessage([FromBody] CreateMessageCommand command, [FromServices] IHubContext<MessageHub> hub)
        {
            var message = await CommandAsync(command);

            await hub.Clients.Group(command.ChatId.ToString())
                .SendAsync("recieveMessage", new
                {
                    ChatId = command.ChatId,
                    Text = message.Text,
                    UserId = message.UserId,
                    Firstname = message.Firstname,
                    Lastname = message.Lastname,
                    Timestamp = message.Timestamp.ToString("dd/MM/yyyy hh:mm:ss")
                });

            await hub.Clients.All
                .SendAsync("UpdateChats", new
                {
                    UserId = message.UserId
                });

            return Ok(message);
        }

        [HttpGet("details")]
        public async Task<IActionResult> GetChatDetailsAsync([FromQuery] GetChatDetailsQuery query)
        {
            await CommandAsync(new MarkAsSeenCommand { ChatId = query.ChatId });

            return Ok(await QueryAsync(query));
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchChatsAsync([FromQuery] SearchChatsQuery query)
        {
            return Ok(await QueryAsync(query));
        }

        [HttpGet("search-for-admin")]
        public async Task<IActionResult> SearchChatsForAdminAsync([FromQuery] SearchChatsForAdminQuery query)
        {
            return Ok(await QueryAsync(query));
        }

        [HttpGet("details-for-admin")]
        public async Task<IActionResult> GetChatDetailsForAdminAsync([FromQuery] GetChatDetailsForAdminQuery query)
        {
            return Ok(await QueryAsync(query));
        }
    }
}
