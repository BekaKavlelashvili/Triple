using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.Shared;
using Triple.Shared.Results;

namespace Triple.Application.Commands.SupportUser
{
    [Validate(typeof(UpdateSupportUserCommandValidator))]
    public class UpdateSupportUserCommand : CommandValidator, IRequest<Result>
    {
        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }


        public override async Task CommandMustBeValidAsync()
        {
            await base.CommandMustBeValidAsync(this);
        }
    }

    public class UpdateSupportUserCommandValidator : AbstractValidator<UpdateSupportUserCommand>
    {
        public UpdateSupportUserCommandValidator()
        {

        }
    }
}
