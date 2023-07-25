using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.Pack;

namespace Triple.Application.Queries.Pack
{
    public class GetPackDetailsQuery : IRequest<PackDto?>
    {
        public Guid EntityId { get; set; }
    }
}
