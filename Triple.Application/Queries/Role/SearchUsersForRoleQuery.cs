using Triple.Application.Dtos.User;
using Triple.Application.Shared;
using Triple.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Application.Queries.Role
{
    public class SearchUsersForRoleQuery : SortAndPagingQuery, IRequest<QueryResultOfList<UserDto>>
    {
        public string? Name { get; set; }

        public string RoleId { get; set; }
    }
}
