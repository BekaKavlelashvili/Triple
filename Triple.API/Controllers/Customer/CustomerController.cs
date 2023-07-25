using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Triple.API.Shared;
using Triple.Application.Commands.Customer;
using Triple.Application.Queries.Customer;

namespace Triple.API.Controllers.Customer
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : BaseController
    {
        public CustomerController(IMediator mediator) : base(mediator)
        {

        }

        [HttpPost]
        //[AuthPermission("AddCustomer")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCustomerCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }

        [HttpPut]
        //[AuthPermission("UpdateCustomer")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateCustomerCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }

        [HttpDelete]
        //[AuthPermission("DeleteCustomer")]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteCustomerCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }
        
        [HttpPut("email")]
        //[AuthPermission("UpdateEmail")]
        public async Task<IActionResult> UpdateEmailAsync([FromBody] UpdateCustomerEmailCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }
        
        [HttpPut("mobileNumber")]
        //[AuthPermission("UpdateMobileNumber")]
        public async Task<IActionResult> UpdateMobileNumberAsync([FromBody] UpdateCustomerMobileNumberCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }
        
        [HttpPut("name")]
        //[AuthPermission("UpdateName")]
        public async Task<IActionResult> UpdateNameAsync([FromBody] UpdateCustomerNameCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }

        [HttpGet("Search")]
        //[AuthPermission("ViewAllCustomers")]
        public async Task<IActionResult> SearchCustomers([FromQuery] SearchCustomersForAdminQuery query)
        {
            return Ok(await QueryAsync(query));
        }

        [HttpGet("details")]
        //[AuthPermission("ViewCustomerDetails")]
        public async Task<IActionResult> GetCustomerDetails([FromQuery] GetCustomerDetailsQuery query)
        {
            return Ok(await QueryAsync(query));
        }
    }
}
