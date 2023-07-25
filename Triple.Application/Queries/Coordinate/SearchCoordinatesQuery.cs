using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.Coordinate;
using Triple.Application.Shared;
using Triple.Shared;

namespace Triple.Application.Queries.Coordinate
{
    public class SearchCoordinatesQuery : SortAndPagingQuery, IRequest<QueryResultOfList<CoordinateDto>>
    {
    }
}
