using Triple.Application.Dtos.Shared;
using Triple.Shared.Resources;
using Triple.Shared.Results;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Application.Commands.Role
{
    [Validate(typeof(CreateRoleCommandValidator))]
    public class CreateRoleCommand : CommandValidator, IRequest<Result>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public override async Task CommandMustBeValidAsync()
        {
            await base.CommandMustBeValidAsync(this);
        }
    }

    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ApplicationStrings.NameIsRequired);
            RuleFor(x => x.Description).NotEmpty().WithMessage(ApplicationStrings.DescriptionIsRequired);
        }
    }
}
