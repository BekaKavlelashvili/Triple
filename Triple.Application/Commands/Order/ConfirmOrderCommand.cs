using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.Shared;
using Triple.Domain.Aggregates.Order.Enum;
using Triple.Shared.Results;

namespace Triple.Application.Commands.Order
{
    [Validate(typeof(ConfirmOrderCommandValidator))]
    public class ConfirmOrderCommand : CommandValidator, IRequest<Result>
    {
        public Guid OrderId { get; set; }

        public OrderStatus Status { get; set; }

        public override async Task CommandMustBeValidAsync()
        {
            await base.CommandMustBeValidAsync(this);
        }
    }

    public class ConfirmOrderCommandValidator : AbstractValidator<ConfirmOrderCommand>
    {
        public ConfirmOrderCommandValidator()
        {

        }
    }
}
