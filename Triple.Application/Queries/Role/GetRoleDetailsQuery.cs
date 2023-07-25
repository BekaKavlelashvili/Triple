using Triple.Application.Dtos.Role;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Triple.Application.Queries.Role
{
    public class GetRoleDetailsQuery : IRequest<RoleDetailsDto?>
    {
        public GetRoleDetailsQuery(string roleId)
        {
            RoleId = roleId;
        }

        public string RoleId { get; set; }
    }
}
