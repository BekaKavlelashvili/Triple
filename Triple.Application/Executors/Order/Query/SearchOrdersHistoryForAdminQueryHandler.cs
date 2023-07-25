using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.Order;
using Triple.Application.Queries.Order;
using Triple.Application.Shared;
using Triple.Infrastructure.Persistence;
using Triple.Shared;

namespace Triple.Application.Executors.Order.Query
{
    public class SearchOrdersHistoryForAdminQueryHandler : IRequestHandler<SearchOrdersHistoryForAdminQuery, QueryResultOfList<OrderForAdminDto>>
    {
        private TripleDbContext _dbContext;

        public SearchOrdersHistoryForAdminQueryHandler(TripleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<QueryResultOfList<OrderForAdminDto>> Handle(SearchOrdersHistoryForAdminQuery request, CancellationToken cancellationToken)
        {
            var orders = await (from pack in _dbContext.Packs
                                from order in _dbContext.Orders.Where(x => x.PackId == pack.EntityId && x.Status != Domain.Aggregates.Order.Enum.OrderStatus.Confirmed)
                                from customer in _dbContext.Users.Where(x => x.OwnerId == order.CustomerId)
                                select new OrderForAdminDto()
                                {
                                    CollectTimeFromDate = order.CollectTimeFromDate,
                                    CollectTimeToDate = order.CollectTimeToDate,
                                    Code = order.Code,
                                    Status = order.Status,
                                    PackId = order.PackId,
                                    Price = order.Price,
                                    PackName = pack.Name,
                                    Quantity = order.Quantity,
                                    CreateDate = order.CreateDate,
                                    UserFirstName = customer.FirstName,
                                    UserLastName = customer.LastName
                                }).ToListAsync();

            if ((request.FromDate != DateTime.MinValue && request.FromDate.HasValue) && (request.ToDate.HasValue && request.ToDate != DateTime.MinValue))
                orders = orders.Where(x => x.CollectTimeFromDate >= request.FromDate && x.CollectTimeToDate <= request.ToDate).ToList();

            var result = orders.AsQueryable().FilterAndSort(request.Filters, request.Sortings).ToPaging(request.Page, request.PageSize).ToList();

            return new QueryResultOfList<OrderForAdminDto>
            {
                Records = result,
                Total = orders.Count()
            };
        }
    }
}
