using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.Shared;
using Triple.Shared.Results;

namespace Triple.Application.Commands.Pack
{
    [Validate(typeof(AddReviewCommandValidator))]
    public class AddReviewCommand : CommandValidator, IRequest<Result>
    {
        public Guid PackId { get; set; }

        public int Rate { get; set; }

        public string? Comment { get; set; }

        public override async Task CommandMustBeValidAsync()
        {
            await base.CommandMustBeValidAsync(this);
        }
    }

    public class AddReviewCommandValidator : AbstractValidator<AddReviewCommand>
    {
        public AddReviewCommandValidator()
        {

        }
    }
}
