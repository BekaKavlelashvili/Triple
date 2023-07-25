using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.Customer;
using Triple.Application.Shared;
using Triple.Shared;

namespace Triple.Application.Queries.Customer
{
    public class SearchCustomersForAdminQuery : SortAndPagingQuery, IRequest<QueryResultOfList<CustomersForAdminDto>>
    {
    }
}
