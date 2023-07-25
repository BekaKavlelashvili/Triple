using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Domain.Shared;

namespace Triple.Domain.Aggregates.Customer
{
    public class Customer : AggregateRoot
    {
        public Customer()
        {

        }

        public Customer(Guid EntityId) : base(EntityId)
        {

        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }

        public string Country { get; set; }


        public void Create(string firstname, string lastname, string email, string mobileNumber, string country)
        {
            FirstName = firstname;
            LastName = lastname;
            Email = email;
            MobileNumber = mobileNumber;
            Country = country;
        }

        public void Update(string firstname, string lastname, string email, string mobileNumber)
        {
            FirstName = firstname;
            LastName = lastname;
            Email = email;
            MobileNumber = mobileNumber;
        }
    }
}
