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
    [Validate(typeof(ResetPasswordCommandValidator))]
    public class ResetPasswordCommand : CommandValidator, IRequest<Result>
    {
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }

        public override async Task CommandMustBeValidAsync()
        {
            await base.CommandMustBeValidAsync(this);
        }
    }

    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            //RuleFor(x => x.Email).NotEmpty().WithMessage(ApplicationStrings.EmailIsRequired);
            //RuleFor(x => x.Email).EmailAddress().WithMessage(ApplicationStrings.EmailFormatIsNotValid);
            //RuleFor(x => x.Password).NotEmpty().WithMessage(ApplicationStrings.PasswordIsRequired);
            //RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage(ApplicationStrings.RepeatePasswordIsRequired);
            //RuleFor(x => x.Password).Equal(x => x.ConfirmPassword).WithMessage(ApplicationStrings.RepeatePasswordInCorrect);
        }
    }
}
