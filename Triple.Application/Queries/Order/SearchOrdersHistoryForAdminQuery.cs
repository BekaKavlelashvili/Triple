using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.Order;
using Triple.Application.Shared;
using Triple.Shared;

namespace Triple.Application.Queries.Order
{
    public class SearchOrdersHistoryForAdminQuery : SortAndPagingQuery, IRequest<QueryResultOfList<OrderForAdminDto>>
    {
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}
