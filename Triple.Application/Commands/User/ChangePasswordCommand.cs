using FluentValidation;
using Triple.Application.Dtos.Shared;
using Triple.Shared.Resources;
using Triple.Shared.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Application.Commands.User
{
    [Validate(typeof(ChangePasswordCommandValidator))]
    public class ChangePasswordCommand : CommandValidator, IRequest<Result>
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string RepeatePassword { get; set; }

        public override async Task CommandMustBeValidAsync()
        {
            await base.CommandMustBeValidAsync(this);
        }
    }

    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            //RuleFor(x => x.Email).NotEmpty().WithMessage(ApplicationStrings.EmailIsRequired);
            //RuleFor(x => x.Email).EmailAddress().WithMessage(ApplicationStrings.EmailFormatIsNotValid);
            //RuleFor(x => x.OldPassword).NotEmpty().WithMessage(ApplicationStrings.PasswordIsRequired);
            //RuleFor(x => x.NewPassword).NotEmpty().WithMessage(ApplicationStrings.PasswordIsRequired);
            //RuleFor(x => x.RepeatePassword).NotEmpty().WithMessage(ApplicationStrings.RepeatePasswordIsRequired);
            //RuleFor(x => x.NewPassword).Equal(x => x.RepeatePassword).WithMessage(ApplicationStrings.RepeatePasswordInCorrect);
        }
    }
}

