using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.Customer;
using Triple.Application.Dtos.Order;
using Triple.Application.Dtos.Organisation;
using Triple.Application.Dtos.Pack;
using Triple.Application.Queries.Customer;
using Triple.Application.Shared;
using Triple.Domain.Aggregates.Order;
using Triple.Domain.Aggregates.Organisation;
using Triple.Infrastructure.Persistence;
using Triple.Shared;

namespace Triple.Application.Executors.Customer.Query
{
    public class SearchCustomersForAdminQueryHandler : IRequestHandler<SearchCustomersForAdminQuery, QueryResultOfList<CustomersForAdminDto>>
    {
        private TripleDbContext _dbContext;

        public SearchCustomersForAdminQueryHandler(TripleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<QueryResultOfList<CustomersForAdminDto>> Handle(SearchCustomersForAdminQuery request, CancellationToken cancellationToken)
        {
            var customers = await (from customer in _dbContext.Customers
                                   select new CustomersForAdminDto()
                                   {
                                       EntityId = customer.EntityId,
                                       Country = customer.Country,
                                       Email = customer.Email,
                                       FirstName = customer.FirstName,
                                       LastName = customer.LastName,
                                       MobileNumber = customer.MobileNumber
                                   }).ToListAsync();

            var orders = await _dbContext.Orders.ToListAsync();

            var organisations = await _dbContext.Organisations.ToListAsync();

            var packs = await _dbContext.Packs.ToListAsync();

            var packOrganisations = new List<Domain.Aggregates.Organisation.Organisation>();

            customers.ForEach(customer =>
            {
                var customerOrders = orders.Where(x => x.CustomerId == customer.EntityId).ToList();

                customerOrders.ForEach(order =>
                {
                    var orderPacks = packs.Where(x => x.EntityId == order.PackId).ToList();
                    orderPacks.ForEach(pack =>
                    {
                        var org = organisations.FirstOrDefault(x => x.EntityId == pack.OrganisationId);
                        packOrganisations.Add(org);
                    });
                });

                customer.OrdersMade = customerOrders.Count;
                customer.RestourantsQuantity = packOrganisations.Distinct().ToList().Count();
            });

            var result = customers.AsQueryable().FilterAndSort(request.Filters, request.Sortings).ToPaging(request.Page, request.PageSize).ToList();

            return new QueryResultOfList<CustomersForAdminDto>
            {
                Records = result,
                Total = customers.Count()
            };
        }
    }
}
