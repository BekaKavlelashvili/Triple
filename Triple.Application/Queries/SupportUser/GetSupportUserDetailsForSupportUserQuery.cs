using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.SupportUSer;

namespace Triple.Application.Queries.SupportUser
{
    public class GetSupportUserDetailsForSupportUserQuery : IRequest<SupportUserDto?>
    {
        public Guid EntityId { get; set; }
    }
}
