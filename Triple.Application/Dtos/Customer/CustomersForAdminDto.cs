using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Application.Dtos.Customer
{
    public class CustomersForAdminDto
    {
        public Guid EntityId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MobileNumber { get; set; }

        public string Email { get; set; }

        public string Country { get; set; }

        public int OrdersMade { get; set; }

        public int RestourantsQuantity { get; set; }
    }
}
