using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.Shared;
using Triple.Shared.Results;

namespace Triple.Application.Commands.Favorite
{
    [Validate(typeof(CreateFavoriteCommandValidator))]
    public class CreateFavoriteCommand : CommandValidator, IRequest<Result>
    {
        public Guid PackId { get; set; }

        public override async Task CommandMustBeValidAsync()
        {
            await base.CommandMustBeValidAsync(this);
        }
    }

    public class CreateFavoriteCommandValidator : AbstractValidator<CreateFavoriteCommand>
    {
        public CreateFavoriteCommandValidator()
        {

        }
    }
}
