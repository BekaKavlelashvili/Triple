//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Http.Features.Authentication;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;
//using Collabiz.Application.Dtos.User;
//using Collabiz.Infrastructure.Identity;
//using Collabiz.Infrastructure.Persistence;
//using Collabiz.Shared;
//using Collabiz.Shared.Constants;
//using Collabiz.Shared.Results;
//using Collabiz.Shared.Password;

//namespace Collabiz.Application.Services
//{
//    public class IdentityService : IIdentityService
//    {
//        private IConfiguration _configuration;
//        private CollabizDbContext _db;
//        private IPasswordHasher _passwordHasher;

//        public IdentityService(CollabizDbContext db, IConfiguration configuration, IPasswordHasher passwordHasher)
//        {
//            _configuration = configuration;
//            _db = db;
//            _passwordHasher = passwordHasher;
//        }

//        public async Task<AuthenticationResult> Authenticate(LoginUserDto dto)
//        {
//            var user = await _db.Set<ApplicationUser>().Include(x => x.Roles).FirstOrDefaultAsync(x => x.Email == dto.Email);

//            if (user == null)
//                return AuthenticationResult.Failed();

//            var signInResult = _passwordHasher.PasswordMatches(dto.Password, user.PasswordHash);

//            if (!signInResult)
//                return AuthenticationResult.Failed();

//            var token = await generateJwtToken(user);

//            var roleIds = user.Roles.Select(x => x.RoleId).ToList();
//            var userRoles = await _db.Set<UserRole>().Where(x => roleIds.Contains(x.Id)).Select(x => x.Name).ToListAsync();
//            var userPermissions = await (from role in _db.Roles
//                                         where roleIds.Contains(role.Id)
//                                         from permission in role.Permissions
//                                         select permission.Permission.PermissionKey).Distinct().ToListAsync();

//            return AuthenticationResult.Succeed(token, userRoles, userPermissions);
//        }

//        public ApplicationUser GetById(string id)
//        {
//            var user = _db.Users.FirstOrDefault(x => x.Id == id);
//            return user;
//        }

//        private async Task<string> generateJwtToken(ApplicationUser user)
//        {
//            var secret = _configuration["Jwt:Secret"];
//            var issuer = _configuration["Jwt:Issuer"];
//            var audience = _configuration["Jwt:Audience"];

//            var roleIds = user.Roles.Select(x => x.RoleId).ToList();

//            var permissionIds = (await _db.Set<RolePermissions>()
//                                           .Where(x => roleIds.Contains(x.RoleId))
//                                           .Select(x => x.PermissionId)
//                                           .ToListAsync())
//                                           .Distinct();

//            var permissions = await _db.Set<Permission>().Where(x => permissionIds.Contains(x.Id)).Select(x => x.PermissionKey).ToListAsync();

//            var permissionsToString = JsonConvert.SerializeObject(permissions);
//            var claims = new List<Claim>();
//            claims.Add(new Claim(ClaimConstants.Email, user.Email));
//            claims.Add(new Claim(ClaimConstants.Id, user.Id));
//            claims.Add(new Claim(ClaimConstants.FirstName, user.FirstName));
//            claims.Add(new Claim(ClaimConstants.LastName, user.LastName));
//            claims.Add(new Claim(ClaimConstants.Permissions, permissionsToString));

//            var key = Encoding.UTF8.GetBytes(secret);

//            var securityKey = new SymmetricSecurityKey(key);
//            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//            var token = new JwtSecurityToken(issuer: issuer,
//                                             audience: audience,
//                                             claims: claims,
//                                             expires: DateTime.UtcNow.AddHours(4).AddDays(1),
//                                             signingCredentials: credentials);

//            var tokenHandler = new JwtSecurityTokenHandler();

//            return tokenHandler.WriteToken(token);
//        }
//    }
//    public class AuthenticationResult
//    {
//        public string Token { get; set; }

//        public bool Success { get; set; }

//        public List<string> UserRoles { get; set; }

//        public List<string> UserPermissions { get; set; }

//        public static AuthenticationResult Failed() => new AuthenticationResult();

//        public static AuthenticationResult Succeed(string token, List<string> userRoles, List<string> userPermissions) => new AuthenticationResult { Success = true, Token = token, UserRoles = userRoles, UserPermissions = userPermissions };
//    }

//    public interface IIdentityService
//    {
//        Task<AuthenticationResult> Authenticate(LoginUserDto dto);

//        ApplicationUser GetById(string id);
//    }
//}
