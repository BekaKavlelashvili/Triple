using MediatR;
using Microsoft.AspNetCore.Mvc;
using Triple.Application.Commands.Favorite;
using Triple.Application.Queries.Favorite;

namespace Triple.API.Controllers.Favorite
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class FavoriteController : BaseController
    {
        public FavoriteController(IMediator mediator) : base(mediator)
        {

        }

        [HttpPost]
        //[AuthPermission("AddFavorite")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateFavoriteCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }

        [HttpDelete]
        //[AuthPermission("DeleteFavorite")]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteFavoriteCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }

        [HttpGet("Search")]
        //[AuthPermission("ViewFavorites")]
        public async Task<IActionResult> SearchFavoritePacks([FromQuery] SearchFavoriteQuery query)
        {
            return Ok(await QueryAsync(query));
        }
    }
}
