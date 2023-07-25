using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Triple.Shared.Constants;

namespace Triple.API.Shared
{
    public class AuthPermissionAttribute : ActionFilterAttribute
    {
        private string[] _permissions;
        public AuthPermissionAttribute(params string[] permissions)
        {
            this._permissions = permissions;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var permissionJson = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimConstants.Permissions).Value;
            var permissions = JsonConvert.DeserializeObject<List<string>>(permissionJson);

            if (!HasPermission(permissions))
            {
                context.Result = new JsonResult(new { message = "DoesNotHavePermission", RequiredPermissions = this._permissions })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }

            await base.OnActionExecutionAsync(context, next);
        }

        private bool HasPermission(List<string> permissions)
        {
            foreach (var permission in _permissions)
            {
                if (!permissions.Contains(permission))
                    return false;
            }

            return true;
        }
    }
}
