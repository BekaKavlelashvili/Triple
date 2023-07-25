using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Domain.Shared;

namespace Triple.Domain.Aggregates.Coordinate
{
    public class Coordinate : AggregateRoot
    {
        public Coordinate()
        {

        }

        public Coordinate(Guid EntityId) : base(EntityId)
        {

        }

        public Guid OrganisationId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }


        public void Create(Guid organisationId, double latitude, double longitude)
        {
            OrganisationId = organisationId;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
