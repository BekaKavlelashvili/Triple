using Triple.Application.Dtos.Role;
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
    public class SearchRolesQuery : SortAndPagingQuery, IRequest<QueryResultOfList<RoleDto>>
    {
        public string? Search { get; set; }
    }
}
