using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Domain.Shared
{
    public class DomainEventBase
    {
        public int Id { get; set; }

        public Guid EntityId { get; set; }

        public DateTime DateOccurred { get; set; }
    }

    public interface IHasDomainEvents
    {
        List<DomainEventBase> Events { get; set; }
    }
}
