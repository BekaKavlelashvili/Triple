using Triple.Application.Commands.User;
using Triple.Infrastructure.Identity;
using Triple.Shared;
using Triple.Shared.Resources;
using Triple.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Application.Executors.User.Command
{
    public class ResetPasswordCommandExecutor : Executor, IRequestHandler<ResetPasswordCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ResetPasswordCommandExecutor(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            //if (user is null)
                //return Failed(ApplicationStrings.UserDoesNotExist);

            var resetPassResult = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            if (!resetPassResult.Succeeded)
                return Failed(resetPassResult.Errors.FirstOrDefault().Description);

            return Succeeded();
        }
    }
}
