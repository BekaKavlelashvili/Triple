using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Domain.Aggregates.Order.Enum;
using Triple.Domain.Shared;

namespace Triple.Domain.Aggregates.Order
{
    public class Order : AggregateRoot
    {
        public Order()
        {

        }

        public Order(Guid EntityId) : base(EntityId)
        {

        }

        public Guid PackId { get; set; }

        public Guid CustomerId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string Code { get; set; }

        public DateTime CollectTimeFromDate { get; set; }

        public DateTime CollectTimeToDate { get; set; }

        public OrderStatus Status { get; set; }

        public void Create(
            Guid packId,
            Guid customerId,
            decimal price,
            int quantity,
            string code,
            DateTime collectTimeFromDate,
            DateTime collectTimeToDate)
        {
            PackId = packId;
            CustomerId = customerId;
            Price = price;
            Quantity = quantity;
            Code = code;
            CollectTimeFromDate = collectTimeFromDate;
            CollectTimeToDate = collectTimeToDate;
        }
        
        public void Update(
            Guid packId,
            decimal price,
            int quantity,
            string code,
            DateTime collectTimeFromDate,
            DateTime collectTimeToDate)
        {
            PackId = packId;
            Price = price;
            Quantity = quantity;
            Code = code;
            CollectTimeFromDate = collectTimeFromDate;
            CollectTimeToDate = collectTimeToDate;
        }
    }
}
