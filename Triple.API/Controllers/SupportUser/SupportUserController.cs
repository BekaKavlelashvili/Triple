using MediatR;
using Microsoft.AspNetCore.Mvc;
using Triple.Application.Commands.SupportUser;
using Triple.Application.Queries.SupportUser;

namespace Triple.API.Controllers.SupportUser
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SupportUserController : BaseController
    {
        public SupportUserController(IMediator mediator) : base(mediator)
        {

        }

        [HttpPost]
        //[AuthPermission("AddSupportUser")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateSupportUserCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }
        
        [HttpPut]
        //[AuthPermission("UpdateSupportUser")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateSupportUserCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }
        
        [HttpDelete]
        //[AuthPermission("DeleteSupportUser")]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteSupportUserCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }
        
        [HttpGet("Search")]
        //[AuthPermission("ViewAllSupportUsers")]
        public async Task<IActionResult> SearchSupportUser([FromQuery] SearchSupportUserQuery query)
        {
            return Ok(await QueryAsync(query));
        }
        
        [HttpGet("Details-For-SupportUser")]
        public async Task<IActionResult> GetSupportUserDetails([FromQuery] GetSupportUserDetailsForSupportUserQuery query)
        {
            return Ok(await QueryAsync(query));
        }
    }
}
