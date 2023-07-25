using MediatR;
using Microsoft.AspNetCore.Mvc;
using Triple.Application.Commands.Coordinate;
using Triple.Application.Queries.Coordinate;

namespace Triple.API.Controllers.Coordinate
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CoordinateController : BaseController
    {
        public CoordinateController(IMediator mediator) : base(mediator)
        {

        }

        [HttpPost]
        //[AuthPermission("CreateCoordinates")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCoordinateCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }

        [HttpGet()]
        //[AuthPermission("ViewCoordinates")]
        public async Task<IActionResult> GetCustomerDetails([FromQuery] SearchCoordinatesQuery query)
        {
            return Ok(await QueryAsync(query));
        }
    }
}
