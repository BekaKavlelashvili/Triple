using Triple.Application.Commands.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Triple.API.Controllers
{
    public class ResetPasswordController : BaseController
    {
        public ResetPasswordController(IMediator mediator) : base(mediator)
        {

        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }

        [HttpGet("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync(string token, string email)
        {
            var command = new ResetPasswordCommand() { Token = token, Email = email};

            return Ok(command);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }
    }
}
