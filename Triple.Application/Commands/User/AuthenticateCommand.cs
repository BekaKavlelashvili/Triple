using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.User;
using Triple.Shared.Results;

namespace Triple.Application.Commands.User
{
    public class AuthenticateCommand : IRequest<Result>
    {
        public LoginUserDto LoginUser { get; set; }
    }
}
