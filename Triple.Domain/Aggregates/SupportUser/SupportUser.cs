using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Domain.Shared;

namespace Triple.Domain.Aggregates.SupportUser
{
    public class SupportUser : AggregateRoot
    {
        public SupportUser()
        {

        }
        public SupportUser(Guid EntityId) : base(EntityId)
        {

        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public void CreateOrUpdate(string firstname, string lastname, string email)
        {
            FirstName = firstname;
            LastName = lastname;
            Email = email;
        }
    }
}
