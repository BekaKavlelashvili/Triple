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
using Triple.Application.Commands.User;
using Triple.Infrastructure.Identity;
using Triple.Shared;
using Triple.Shared.Results;

namespace Triple.Application.Executors.User.Command
{
    public class ChangePasswordCommandExecutor : Executor, IRequestHandler<ChangePasswordCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ChangePasswordCommandExecutor(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            await request.CommandMustBeValidAsync();

            var user = await _userManager.FindByIdAsync(request.Id);

            //if (user is null)
            //    return Failed(ApplicationStrings.UserDoesNotExist);

            var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

            //if(!result.Succeeded)
            //    return Failed(ApplicationStrings.UserPasswordResetFailed);

            return Succeeded();
        }
    }
}
