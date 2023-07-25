using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.Customer;
using Triple.Application.Queries.Customer;
using Triple.Application.Shared;
using Triple.Infrastructure.Persistence;

namespace Triple.Application.Executors.Customer.Query
{
    public class GetCustomerDetailsQueryHandler : IRequestHandler<GetCustomerDetailsQuery, CustomerDto?>
    {
        private TripleDbContext _dbContext;

        public GetCustomerDetailsQueryHandler(TripleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CustomerDto?> Handle(GetCustomerDetailsQuery request, CancellationToken cancellationToken)
        {
            return await (from customer in _dbContext.Customers
                          select new CustomerDto()
                          {
                              EntityId = customer.EntityId,
                              Country = customer.Country,
                              Email = customer.Email,
                              FirstName = customer.FirstName,
                              LastName = customer.LastName,
                              MobileNumber = customer.MobileNumber
                          }).FirstOrDefaultAsync();
        }
    }
}
