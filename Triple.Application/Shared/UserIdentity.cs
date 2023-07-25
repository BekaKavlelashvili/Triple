using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Triple.Application.Shared
{
    public class UserIdentity
    {
        public string UserId { get; }

        public string Email { get; }

        public UserIdentity(string userId, string email)
        {
            UserId = userId;
            Email = email;
        }

        public static UserIdentity From(ClaimsPrincipal user)
        {
            var userId = user.Claims.FirstOrDefault(c => c.Type.Equals("UserId"));
            if (string.IsNullOrWhiteSpace(userId?.Value))
                throw new ArgumentException($"{nameof(userId)} is required");

            var email = user.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email));
            if (string.IsNullOrWhiteSpace(email?.Value))
                throw new ArgumentException($"{nameof(email)} is required");

            return new UserIdentity
            (
                userId.Value,
                email.Value
            );
        }

        public static UserIdentity Empty()
        {
            return new UserIdentity(string.Empty, string.Empty);
        }
    }
}
