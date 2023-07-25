using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.Order;
using Triple.Application.Dtos.Organisation;
using Triple.Application.Queries.Organisation;
using Triple.Application.Shared;
using Triple.Infrastructure.Persistence;
using Triple.Shared;

namespace Triple.Application.Executors.Organisation.Query
{
    public class SearchOrganisationQueryHandler : IRequestHandler<SearchOrganisationQuery, QueryResultOfList<OrganisationDto>>
    {
        private TripleDbContext _dbContext;

        public SearchOrganisationQueryHandler(TripleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<QueryResultOfList<OrganisationDto>> Handle(SearchOrganisationQuery request, CancellationToken cancellationToken)
        {
            var organisations = await (from org in _dbContext.Organisations
                                       select new OrganisationDto()
                                       {
                                           EntityId = org.EntityId,
                                           Category = org.Category,
                                           Commission = org.Commission,
                                           Name = org.Name,
                                           Network = org.Network,
                                           OperatorFirstName = org.OperatorFirstName,
                                           OperatorLastName = org.OperatorLastName,
                                           OperatorPhone = org.OperatorPhone,
                                           OrganisationAddress = org.OrganisationAddress
                                       }).ToListAsync();

            organisations.ForEach(organisation =>
            {
                organisation.Branches = organisations.Count;

                var orders = (from pack in _dbContext.Packs.Where(x => x.OrganisationId == organisation.EntityId)
                              from order in _dbContext.Orders.Where(x => x.PackId == pack.EntityId)
                              select order).ToList();

                organisation.TotalOrders = orders.Count;
                organisation.CurrentOrders = orders.Where(x => x.Status == Domain.Aggregates.Order.Enum.OrderStatus.Current).ToList().Count;
            });

            var result = organisations.AsQueryable().FilterAndSort(request.Filters, request.Sortings).ToPaging(request.Page, request.PageSize).ToList();

            return new QueryResultOfList<OrganisationDto>
            {
                Records = result,
                Total = organisations.Count
            };
        }
    }
}
