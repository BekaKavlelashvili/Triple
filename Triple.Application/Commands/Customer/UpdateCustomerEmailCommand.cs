using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.Shared;
using Triple.Shared.Results;

namespace Triple.Application.Commands.Customer
{
    [Validate(typeof(UpdateCustomerEmailCommandValidator))]
    public class UpdateCustomerEmailCommand : CommandValidator, IRequest<Result>
    {
        public Guid EntityId { get; set; }

        public string Email { get; set; }


        public override async Task CommandMustBeValidAsync()
        {
            await base.CommandMustBeValidAsync(this);
        }        
    }

    public class UpdateCustomerEmailCommandValidator : AbstractValidator<UpdateCustomerEmailCommand>
    {
        public UpdateCustomerEmailCommandValidator()
        {

        }
    }
}
