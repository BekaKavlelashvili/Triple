using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.Shared;
using Triple.Shared.Results;

namespace Triple.Application.Commands.Order
{
    [Validate(typeof(UpdateOrderCommandValidator))]
    public class UpdateOrderCommand : CommandValidator, IRequest<Result>
    {
        public Guid OrderId { get; set; }

        public Guid PackId { get; set; }

        public int Quantity { get; set; }

        public override async Task CommandMustBeValidAsync()
        {
            await base.CommandMustBeValidAsync(this);
        }
    }

    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {

        }
    }
}
