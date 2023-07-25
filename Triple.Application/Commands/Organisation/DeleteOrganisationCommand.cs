﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Shared.Results;

namespace Triple.Application.Commands.Organisation
{
    public class DeleteOrganisationCommand : IRequest<Result>
    {
        public Guid EntityId { get; set; }

        public DeleteOrganisationCommand(Guid entityId)
        {
            EntityId = entityId;
        }
    }
}
