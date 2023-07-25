using Triple.Application.Commands.User;
using Triple.Application.Dtos.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Triple.API.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        public AccountController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> LogInAsync([FromBody] LoginUserDto loginDto)
        {
            var result = await CommandAsync(new AuthenticateCommand() { LoginUser = loginDto });

            if (!result.Succeeded)
                return Unauthorized(result);

            return Ok(result);
        }
    }
}
