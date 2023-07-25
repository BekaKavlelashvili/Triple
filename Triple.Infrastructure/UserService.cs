using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Triple.Infrastructure
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor accessor;

        public UserService(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public ClaimsPrincipal GetUser()
        {
            return accessor?.HttpContext?.User as ClaimsPrincipal;
        }
    }

    public interface IUserService
    {
        ClaimsPrincipal GetUser();
    }
}
