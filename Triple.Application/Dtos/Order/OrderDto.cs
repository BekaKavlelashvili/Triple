using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Domain.Aggregates.Order.Enum;

namespace Triple.Application.Dtos.Order
{
    public class OrderDto
    {
        public Guid PackId { get; set; }

        public string PackName { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string Code { get; set; }

        public DateTime CollectTimeFromDate { get; set; }

        public DateTime CollectTimeToDate { get; set; }

        public OrderStatus Status { get; set; }
    }
}
