using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Domain.Shared;

namespace Triple.Domain.Aggregates.Favorite
{
    public class Favorite : AggregateRoot
    {
        public Favorite()
        {

        }
        public Favorite(Guid EntityId) : base(EntityId)
        {

        }

        public Guid CustomerId { get; set; }

        public Guid PackId { get; set; }

        public void Create(Guid customerId, Guid packId)
        {
            CustomerId = customerId;
            PackId = packId;
        }
    }
}
