using Triple.Shared.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Application.Commands.User
{
    public class ForgotPasswordCommand : IRequest<Result>
    {
        public string Email { get; set; }
    }
}
