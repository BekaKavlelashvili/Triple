using Triple.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Domain.Shared
{
    public abstract class AggregateRoot
    {
        public AggregateRoot()
        {
        }

        public AggregateRoot(Guid entityId)
        {
            this.EntityId = Guid.NewGuid();
            this.CreateDate = DateTime.UtcNow.AddHours(4);
            this.LastChangeDate = DateTime.UtcNow.AddHours(4);
        }

        public int Id { get; set; }

        public Guid EntityId { get; set; }

        private List<object> _events { get; set; } = new List<object>();

        public IReadOnlyList<object> GetEvents() => this._events.AsReadOnly().ToList();

        public DateTime CreateDate { get; set; }

        public DateTime LastChangeDate { get; set; }

        public EntityStatus EntityStatus { get; set; }

        //public string? UpdateUser { get; set; }

        public virtual void Delete() => this.EntityStatus = EntityStatus.Deleted;

        public bool IsActive() => this.EntityStatus == EntityStatus.Active;

        public bool HasEvents() => this._events.Any();

        protected void Raise(DomainEventBase @event)
        {
            @event.Id = this.Id;
            @event.EntityId = this.EntityId;
            @event.DateOccurred = DateTime.UtcNow.AddHours(4);

            this._events.Add(@event);
        }

        public void UpdateLastChangeDate()
        {
            this.LastChangeDate = DateTime.UtcNow.AddHours(4);
        }

        public void ClearEvents() => this._events.Clear();
    }
}
