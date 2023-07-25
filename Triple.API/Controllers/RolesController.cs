using Triple.API.Shared;
using Triple.Application.Commands.Role;
using Triple.Application.Queries.Role;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Triple.API.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : BaseController
    {
        public RolesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [AuthPermission("AddNewRole")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateRoleCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }

        [HttpPut]
        [AuthPermission("EditRole")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateRoleCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }

        [HttpDelete("{id}")]
        [AuthPermission("DeleteRole")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return ToHttpResponse(await CommandAsync(new DeleteRoleCommand(id)));
        }

        [HttpGet("search-roles")]
        [AuthPermission("ViewRolesList")]
        public async Task<IActionResult> SearchRolesAsync([FromQuery] SearchRolesQuery query)
        {
            FillPaging(query);

            return Ok(await QueryAsync(query));
        }

        [HttpGet("{id}/details")]
        [AuthPermission("GetRole")]
        public async Task<IActionResult> GetRoleDetailsAsync(string id)
        {
            return Ok(await QueryAsync(new GetRoleDetailsQuery(id)));
        }

        [HttpGet("get-permissions")]
        [AuthPermission("ViewRolePermissions")]
        public async Task<IActionResult> SearchPermissionsAsync()
        {
            return Ok(await QueryAsync(new GetPermissionsQuery()));
        }

        [HttpPost("set-permissions")]
        [AuthPermission("AddPermissionToRole")]
        public async Task<IActionResult> SetPermissionsAsync([FromBody] SetPermissionCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }

        [HttpDelete("delete-permission")]
        [AuthPermission("RemovePermissionToRole")]
        public async Task<IActionResult> DeletePermissionAsync([FromBody] DeletePermissionCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }

        [HttpGet("search-users")]
        [AuthPermission("ViewRoleUsers")]
        public async Task<IActionResult> SearchUsersAsync([FromQuery] SearchUsersForRoleQuery query)
        {
            FillPaging(query);

            return Ok(await QueryAsync(query));
        }

        [HttpPost("set-users")]
        [AuthPermission("AddUsersToRole")]
        public async Task<IActionResult> SetUsersAsync([FromBody] SetUsersCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }

        [HttpDelete("delete-user")]
        [AuthPermission("RemoveUsersFromRole")]
        public async Task<IActionResult> DeleteUserAsync([FromBody] DeleteUserFromRoleCommand command)
        {
            return ToHttpResponse(await CommandAsync(command));
        }
    }
}
