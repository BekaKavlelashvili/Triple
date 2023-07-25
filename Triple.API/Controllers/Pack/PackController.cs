using Triple.API.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Triple.Application.Commands.Pack;
using Triple.Application.Queries.Pack;

namespace Triple.API.Controllers.Pack
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PackController : BaseController
    {
        public PackController(IMediator mediator) : base(mediator)
        {

        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        //[AuthPermission("AddPack")]
        public async Task<IActionResult> CreateAsync([FromForm] CreatePackCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }
        
        [HttpPost("Priority")]
        //[AuthPermission("AddPackPriority")]
        public async Task<IActionResult> AddPackPriorityAsync([FromBody] AddPriorityPackCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }
        
        [HttpPost("Review")]
        //[AuthPermission("AddPackReview")]
        public async Task<IActionResult> AddPackReviewAsync([FromBody] AddReviewCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }

        [HttpGet("Review")]
        //[AuthPermission("ViewReviews")]
        public async Task<IActionResult> GetReview([FromQuery] GetPackReviewQuery query)
        {
            return Ok(await QueryAsync(query));
        }


        [HttpPut]
        [Consumes("multipart/form-data")]
        //[AuthPermission("UpdatePack")]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdatePackCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }

        [HttpDelete]
        //[AuthPermission("DeletePack")]
        public async Task<IActionResult> UpdateAsync([FromBody] DeletePackCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }

        [HttpGet("Search")]
        //[AuthPermission("ViewAllPacks")]
        public async Task<IActionResult> SearchPacks([FromQuery] SearchPackQuery query)
        {
            return Ok(await QueryAsync(query));
        }

        [HttpGet("Details")]
        //[AuthPermission("ViewPackDetails")]
        public async Task<IActionResult> GetPackDetails([FromQuery] GetPackDetailsQuery query)
        {
            return Ok(await QueryAsync(query));
        }

        [HttpGet("OrganisationCurrentPacks")]
        //[AuthPermission("ViewCurrentPacksForOrganisation")]
        public async Task<IActionResult> SearchOragnisationCurrentPacks([FromQuery] SearchCurrentPackForOrganisationQuery query)
        {
            return Ok(await QueryAsync(query));
        }
        
        [HttpGet("OrganisationAllPacks")]
        //[AuthPermission("ViewAllPacksForOrganisation")]
        public async Task<IActionResult> SearchOragnisationAllPacks([FromQuery] SearchAllPacksForOrganisationQuery query)
        {
            return Ok(await QueryAsync(query));
        }
    }
}
