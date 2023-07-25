using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Domain.Aggregates.Organisation.Enum;
using Triple.Domain.Shared;

namespace Triple.Domain.Aggregates.Organisation
{
    public class Organisation : AggregateRoot
    {
        public Organisation()
        {

        }

        public Organisation(Guid EntityId) : base(EntityId)
        {

        }

        public string Name { get; set; }

        public string Network { get; set; }

        public string Email { get; set; }

        public string OrganisationAddress { get; set; }

        public decimal Commission { get; set; }

        public string OperatorFirstName { get; set; }

        public string OperatorLastName { get; set; }

        public string OperatorPhone { get; set; }

        public OrganisationCategory Category { get; set; }

        public void CreateOrUpdate(
            string name,
            string email,
            string network,
            string organisationAddress,
            decimal commission,
            string operatorFirstName,
            string operatorLastName,
            string operatoPhone,
            OrganisationCategory category)
        {
            Name = name;
            Email = email;
            Network = network;
            OrganisationAddress = organisationAddress;
            Commission = commission;
            OperatorFirstName = operatorFirstName;
            OperatorLastName = operatorLastName;
            OperatorPhone = operatoPhone;
        }
    }
}
