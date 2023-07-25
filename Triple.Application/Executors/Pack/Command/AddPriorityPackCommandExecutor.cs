using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Commands.Pack;
using Triple.Infrastructure.Persistence;
using Triple.Shared;
using Triple.Shared.Results;

namespace Triple.Application.Executors.Pack.Command
{
    public class AddPriorityPackCommandExecutor : Executor, IRequestHandler<AddPriorityPackCommand, Result>
    {
        private TripleDbContext _db;

        public AddPriorityPackCommandExecutor(TripleDbContext db)
        {
            _db = db;
        }

        public async Task<Result> Handle(AddPriorityPackCommand request, CancellationToken cancellationToken)
        {
            await request.CommandMustBeValidAsync();

            var pack = await _db.Packs.FirstOrDefaultAsync(x => x.EntityId == request.PackId);

            if (pack is null)
                return NotFound();

            pack.IsPriority = request.IsPriority;

            await _db.SaveChangesAsync();

            return Succeed();
        }
    }
}
