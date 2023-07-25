using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.Customer;

namespace Triple.Application.Queries.Customer
{
    public class GetCustomerDetailsQuery : IRequest<CustomerDto?>
    {
        public Guid EntityId { get; set; }
    }
}
