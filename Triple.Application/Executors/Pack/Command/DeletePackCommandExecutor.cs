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
    public class DeletePackCommandExecutor : Executor, IRequestHandler<DeletePackCommand, Result>
    {
        private TripleDbContext _db;

        public DeletePackCommandExecutor(TripleDbContext db)
        {
            _db = db;
        }

        public async Task<Result> Handle(DeletePackCommand request, CancellationToken cancellationToken)
        {
            var pack = await _db.Packs.FirstOrDefaultAsync(x => x.EntityId == request.EntityId);

            if (pack is null)
                return NotFound();

            _db.Packs.Remove(pack);

            await _db.SaveChangesAsync();

            return Succeed();
        }
    }
}
