using Triple.Application.Commands.User;
using Triple.Application.Services.MailService;
using Triple.Infrastructure.Identity;
using Triple.Shared;
using Triple.Shared.Resources;
using Triple.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Application.Executors.User.Command
{
    public class ForgotPasswordCommandExecutor : Executor, IRequestHandler<ForgotPasswordCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMailService _mailService;
        private readonly IHttpContextAccessor _accessor;
        private readonly LinkGenerator _generator;
        private readonly IConfiguration _configuration;

        public ForgotPasswordCommandExecutor(UserManager<ApplicationUser> userManager, IMailService mailService, IHttpContextAccessor accessor, LinkGenerator generator, IConfiguration configuration)
        {
            _userManager = userManager;
            _mailService = mailService;
            _accessor = accessor;
            _generator = generator;
            _configuration = configuration;
        }

        public async Task<Result> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            //if (user is null)
                //return Failed(ApplicationStrings.UserDoesNotExist);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var appUrl = _configuration.GetSection("AppUrl");

            var link = _generator.GetUriByPage(_accessor.HttpContext,
                values: new { token, Email = user.Email },
                scheme: _accessor.HttpContext.Request.Scheme);

            link = link.Replace(String.Format("{0}://{1}/", _accessor.HttpContext.Request.Scheme, _accessor.HttpContext.Request.Host.Value), appUrl.Value);
            link = link.Replace("forgot-password", "auth/reset-password");

            var mailRequest = new MailRequest();

            mailRequest.ToEmail = request.Email;
            mailRequest.Subject = "Password Reset";
            mailRequest.Body = link;

            await _mailService.SendEmailAsync(mailRequest);

            return Succeeded(user.Id);
        }
    }
}
