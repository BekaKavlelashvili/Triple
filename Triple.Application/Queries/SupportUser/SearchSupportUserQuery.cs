using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.SupportUSer;
using Triple.Application.Shared;
using Triple.Shared;

namespace Triple.Application.Queries.SupportUser
{
    public class SearchSupportUserQuery : SortAndPagingQuery, IRequest<QueryResultOfList<SupportUserDto>>
    {
    }
}
