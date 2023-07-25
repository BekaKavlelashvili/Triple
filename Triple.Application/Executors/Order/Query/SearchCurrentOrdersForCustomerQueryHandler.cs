﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.Order;
using Triple.Application.Dtos.Pack;
using Triple.Application.Queries.Order;
using Triple.Application.Shared;
using Triple.Infrastructure;
using Triple.Infrastructure.Persistence;
using Triple.Shared;

namespace Triple.Application.Executors.Order.Query
{
    public class SearchCurrentOrdersForCustomerQueryHandler : IRequestHandler<SearchCurrentOrdersForCustomerQuery, QueryResultOfList<OrderDto>>
    {
        private TripleDbContext _dbContext;
        private readonly IUserService _userService;

        public SearchCurrentOrdersForCustomerQueryHandler(TripleDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<QueryResultOfList<OrderDto>> Handle(SearchCurrentOrdersForCustomerQuery request, CancellationToken cancellationToken)
        {
            var currentUserEmail = UserIdentity.From(_userService.GetUser()).Email;

            var currentUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == currentUserEmail);

            var orders = await (from pack in _dbContext.Packs
                                from order in _dbContext.Orders.Where(x => x.CustomerId == currentUser.OwnerId && x.PackId == pack.EntityId && x.Status == Domain.Aggregates.Order.Enum.OrderStatus.Current)
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

            if ((request.FromDate != DateTime.MinValue && request.FromDate.HasValue) && (request.ToDate.HasValue && request.ToDate != DateTime.MinValue))
                orders = orders.Where(x => x.CollectTimeFromDate >= request.FromDate && x.CollectTimeToDate <= request.ToDate).ToList();

            var result = orders.AsQueryable().FilterAndSort(request.Filters, request.Sortings).ToPaging(request.Page, request.PageSize).ToList();

            return new QueryResultOfList<OrderDto>
            {
                Records = result,
                Total = orders.Count()
            };
        }
    }
}
