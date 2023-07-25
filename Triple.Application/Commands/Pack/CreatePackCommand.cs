using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.Pack;
using Triple.Application.Dtos.Shared;
using Triple.Domain.Aggregates.Pack.Enum;
using Triple.Shared.Results;

namespace Triple.Application.Commands.Pack
{
    [Validate(typeof(CreatePackCommandValidator))]
    public class CreatePackCommand : CommandValidator, IRequest<Result>
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public Guid OrganisationId { get; set; }

        public PackType Type { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int Quantity { get; set; }

        public decimal DiscountPrice { get; set; }

        public decimal MinRegularPrice { get; set; }

        public string Description { get; set; }

        public DateTime EndDate { get; set; }

        public List<DayOfWeek> Days { get; set; } = new List<DayOfWeek>();

        public bool IsRepeatable { get; set; }

        public List<IFormFile> Photos { get; set; } = new List<IFormFile>();

        public override async Task CommandMustBeValidAsync()
        {
            await base.CommandMustBeValidAsync(this);
        }
    }

    public class CreatePackCommandValidator : AbstractValidator<CreatePackCommand>
    {
        public CreatePackCommandValidator()
        {

        }
    }
}
