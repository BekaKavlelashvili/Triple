using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.Organisation;
using Triple.Application.Shared;
using Triple.Shared;

namespace Triple.Application.Queries.Organisation
{
    public class SearchOrganisationQuery : SortAndPagingQuery, IRequest<QueryResultOfList<OrganisationDto>>
    {
    }
}
