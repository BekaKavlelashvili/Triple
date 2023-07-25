using MediatR;
using Microsoft.AspNetCore.Mvc;
using Triple.API.Shared;
using Triple.Application.Commands.Order;
using Triple.Application.Executors.Order.Query;
using Triple.Application.Queries.Order;

namespace Triple.API.Controllers.Order
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OrderController : BaseController
    {
        public OrderController(IMediator mediator) : base(mediator)
        {

        }

        [HttpPost]
        //[AuthPermission("AddOrder")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateOrderCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }
        
        [HttpPut]
        //[AuthPermission("UpdateOrder")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateOrderCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }

        [HttpPost("Confirm")]
        //[AuthPermission("ConfirmOrder")]
        public async Task<IActionResult> ConfirmOrderAsync([FromBody] ConfirmOrderCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }

        [HttpDelete]
        //[AuthPermission("DeleteOrder")]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteOrderCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }

        [HttpGet("Search-Current-For-Customer")]
        //[AuthPermission("ViewOrders")]
        public async Task<IActionResult> SearchCurrentOrdersForCustomer([FromQuery] SearchCurrentOrdersForCustomerQuery query)
        {
            return Ok(await QueryAsync(query));
        }

        [HttpGet("Search-History-For-Customer")]
        //[AuthPermission("ViewOrders")]
        public async Task<IActionResult> SearchOrdersHistoryForCustomer([FromQuery] SearchOrdersHistoryForCustomerQuery query)
        {
            return Ok(await QueryAsync(query));
        }

        [HttpGet("Search-Current-For-Organisation")]
        //[AuthPermission("ViewOrders")]
        public async Task<IActionResult> SearchCurrentOrdersForOrganisation([FromQuery] SearchCurrentOrdersForOrganisationQuery query)
        {
            return Ok(await QueryAsync(query));
        }

        [HttpGet("Search-History-For-Organisation")]
        //[AuthPermission("ViewOrders")]
        public async Task<IActionResult> SearchOrdersHistoryForOrganisation([FromQuery] SearchOrdersHistoryForOrganisationQuery query)
        {
            return Ok(await QueryAsync(query));
        }
        
        [HttpGet("Search-Current-For-Admin")]
        //[AuthPermission("ViewOrders")]
        public async Task<IActionResult> SearchCurrentOrdersForAdmin([FromQuery] SearchCurrentOrdersForAdminQuery query)
        {
            return Ok(await QueryAsync(query));
        }
        
        [HttpGet("Search-History-For-Admin")]
        //[AuthPermission("ViewOrders")]
        public async Task<IActionResult> SearchOrdersHistoryForAdmin([FromQuery] SearchOrdersHistoryForAdminQuery query)
        {
            return Ok(await QueryAsync(query));
        }
    }
}
