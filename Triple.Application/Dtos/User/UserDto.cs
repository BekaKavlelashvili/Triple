using System;
using System.Collections.Generic;
using System.Text;

namespace Triple.Application.Dtos.User
{
    public class UserDto
    {
        public string Id { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? UserName { get; set; }
    }
}
