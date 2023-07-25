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
    public class CreateUserCommandExecutor : Executor, IRequestHandler<CreateUserCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateUserCommandExecutor(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _userManager.FindByNameAsync(request.UserName);
            if (userExists is not null)
                return Failed(ApplicationStrings.UserNameMustBeUnique);
            userExists = await _userManager.FindByEmailAsync(request.Email);
            if (userExists is not null)
                return Failed(ApplicationStrings.EmailMustBeUnique);

            ApplicationUser user = new()
            {
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                UserName = request.UserName,
                OwnerId = request.UserId,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            await _userManager.CreateAsync(user, request.Password);

            return Succeeded(user.Id);
        }
    }
}
