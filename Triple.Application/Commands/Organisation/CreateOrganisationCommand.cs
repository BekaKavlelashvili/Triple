using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.Shared;
using Triple.Domain.Aggregates.Organisation.Enum;
using Triple.Shared.Results;

namespace Triple.Application.Commands.Organisation
{
    [Validate(typeof(CreateOrganisationCommandValidator))]
    public class CreateOrganisationCommand : CommandValidator, IRequest<Result>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Network { get; set; }

        public string OrganisationAddress { get; set; }

        public decimal Commission { get; set; }

        public string OperatorFirstName { get; set; }

        public string OperatorLastName { get; set; }

        public string OperatorPhone { get; set; }

        public OrganisationCategory Category { get; set; }

        public override async Task CommandMustBeValidAsync()
        {
            await base.CommandMustBeValidAsync(this);
        }
    }

    public class CreateOrganisationCommandValidator : AbstractValidator<CreateOrganisationCommand>
    {
        public CreateOrganisationCommandValidator()
        {

        }
    }
}
