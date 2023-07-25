using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.Coordinate;
using Triple.Application.Queries.Coordinate;
using Triple.Application.Shared;
using Triple.Infrastructure.Persistence;
using Triple.Shared;

namespace Triple.Application.Executors.Coordinate.Query
{
    public class SearchCoordinateQueryHandler : IRequestHandler<SearchCoordinatesQuery, QueryResultOfList<CoordinateDto>>
    {
        private TripleDbContext _dbContext;

        public SearchCoordinateQueryHandler(TripleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<QueryResultOfList<CoordinateDto>> Handle(SearchCoordinatesQuery request, CancellationToken cancellationToken)
        {
            var coordinates = await (from coordinate in _dbContext.Coordinates
                                     from organisation in _dbContext.Organisations.Where(x=> x.EntityId == coordinate.OrganisationId)
                                     select new CoordinateDto()
                                     {
                                         Address = organisation.OrganisationAddress,
                                         Latitude = coordinate.Latitude,
                                         Longitude = coordinate.Longitude,
                                         OrganisationId = organisation.EntityId,
                                         OrganisationName = organisation.Name
                                     }).ToListAsync();

            var result = coordinates.AsQueryable().FilterAndSort(request.Filters, request.Sortings).ToPaging(request.Page, request.PageSize).ToList();

            return new QueryResultOfList<CoordinateDto>
            {
                Total = coordinates.Count,
                Records = result
            };
        }
    }
}
