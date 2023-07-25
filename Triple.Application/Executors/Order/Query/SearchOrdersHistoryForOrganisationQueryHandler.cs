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
using Triple.Infrastructure;
using Triple.Infrastructure.Persistence;
using Triple.Shared;

namespace Triple.Application.Executors.Order.Query
{
    public class SearchOrdersHistoryForOrganisationQueryHandler : IRequestHandler<SearchOrdersHistoryForOrganisationQuery, QueryResultOfList<OrderDto>>
    {
        private TripleDbContext _dbContext;
        private readonly IUserService _userService;

        public SearchOrdersHistoryForOrganisationQueryHandler(TripleDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<QueryResultOfList<OrderDto>> Handle(SearchOrdersHistoryForOrganisationQuery request, CancellationToken cancellationToken)
        {
            var currentUserEmail = UserIdentity.From(_userService.GetUser()).Email;

            var currentUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == currentUserEmail);

            var orders = await (from organisation in _dbContext.Organisations.Where(x => x.EntityId == currentUser.OwnerId)
                                from pack in _dbContext.Packs.Where(x => x.OrganisationId == organisation.EntityId)
                                from order in _dbContext.Orders.Where(x => x.PackId == pack.EntityId && x.Status != Domain.Aggregates.Order.Enum.OrderStatus.Current)
                                select new OrderDto()
                                {
                                    CollectTimeFromDate = order.CollectTimeFromDate,
                                    CollectTimeToDate = order.CollectTimeToDate,
                                    Code = order.Code,
                                    Status = order.Status,
                                    PackId = order.PackId,
                                    Price = order.Price,
                                    PackName = pack.Name,
                                    Quantity = order.Quantity
                                }).ToListAsync();

            var result = orders.AsQueryable().FilterAndSort(request.Filters, request.Sortings).ToPaging(request.Page, request.PageSize).ToList();

            return new QueryResultOfList<OrderDto>
            {
                Records = result,
                Total = orders.Count()
            };
        }
    }
}
