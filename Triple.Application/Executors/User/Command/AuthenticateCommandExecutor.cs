using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Commands.User;
using Triple.Infrastructure.Identity;
using Triple.Infrastructure.Persistence;
using Triple.Shared;
using Triple.Shared.Constants;
using Triple.Shared.Results;

namespace Triple.Application.Executors.User.Command
{
    public class AuthenticateCommandExecutor : Executor, IRequestHandler<AuthenticateCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<UserRole> _roleManager;
        private readonly IConfiguration _configuration;
        private TripleDbContext _dbContext;

        public AuthenticateCommandExecutor(TripleDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<UserRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.LoginUser.Email);
            if (user is not null && await _userManager.CheckPasswordAsync(user, request.LoginUser.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("UserId", user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var permissions = (await _dbContext.Set<RolePermissions>()
                                           .Where(x => userRoles.Contains(x.Role.Name))
                                           .Select(x => x.PermissionKey)
                                           .ToListAsync())
                                           .Distinct();
                var permissionsToString = JsonConvert.SerializeObject(permissions);

                authClaims.Add(new Claim(ClaimConstants.Permissions, permissionsToString));

                var token = GetToken(authClaims);

                return Succeeded(new
                {
                    userId = user.Id,
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Failed("Unauthorized");
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddHours(4).AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
