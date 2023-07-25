using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Domain.Aggregates.Organisation.Enum;

namespace Triple.Application.Dtos.Organisation
{
    public class OrganisationDto
    {
        public Guid EntityId { get; set; }

        public string Name { get; set; }

        public string Network { get; set; }

        public string OrganisationAddress { get; set; }

        public decimal Commission { get; set; }

        public string OperatorFirstName { get; set; }

        public string OperatorLastName { get; set; }

        public string OperatorPhone { get; set; }

        public int Branches { get; set; }

        public int TotalOrders { get; set; }

        public int CurrentOrders { get; set; }

        public OrganisationCategory Category { get; set; }
    }
}
