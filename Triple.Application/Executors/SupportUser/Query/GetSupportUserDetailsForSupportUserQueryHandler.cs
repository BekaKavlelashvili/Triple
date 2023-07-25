using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.SupportUSer;
using Triple.Application.Queries.SupportUser;
using Triple.Application.Shared;
using Triple.Infrastructure;
using Triple.Infrastructure.Persistence;

namespace Triple.Application.Executors.SupportUser.Query
{
    public class GetSupportUserDetailsForSupportUserQueryHandler : IRequestHandler<GetSupportUserDetailsForSupportUserQuery, SupportUserDto?>
    {
        private TripleDbContext _dbContext;
        private readonly IUserService _userService;

        public GetSupportUserDetailsForSupportUserQueryHandler(TripleDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<SupportUserDto?> Handle(GetSupportUserDetailsForSupportUserQuery request, CancellationToken cancellationToken)
        {
            var currentUserEmail = UserIdentity.From(_userService.GetUser()).Email;

            return await (from user in _dbContext.SupportUsers.Where(x => x.Email == currentUserEmail)
                          select new SupportUserDto()
                          {
                              Email = user.Email,
                              EntityId = user.EntityId,
                              FirstName = user.FirstName,
                              LastName = user.LastName
                          }).FirstOrDefaultAsync();
        }
    }
}
