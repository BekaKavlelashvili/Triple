using Triple.API.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Triple.Application.Commands.Organisation;
using Triple.Application.Queries.Organisation;

namespace Triple.API.Controllers.Organisation
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OrganisationController : BaseController
    {
        public OrganisationController(IMediator mediator) : base(mediator)
        {

        }

        [HttpPost]
        //[AuthPermission("AddOrganisation")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateOrganisationCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }
        
        [HttpPut]
        //[AuthPermission("UpdateOrganisation")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateOrganisationCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }
        
        [HttpDelete]
        //[AuthPermission("DeleteOrganisation")]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteOrganisationCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }

        [HttpGet("Search")]
        //[AuthPermission("ViewAllOrganisations")]
        public async Task<IActionResult> SearchOrganisations([FromQuery] SearchOrganisationQuery query)
        {
            return Ok(await QueryAsync(query));
        }

    }
}
