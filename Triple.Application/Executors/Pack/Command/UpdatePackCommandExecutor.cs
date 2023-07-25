using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Commands.Pack;
using Triple.Application.Services;
using Triple.Infrastructure.Persistence;
using Triple.Shared;
using Triple.Shared.Results;

namespace Triple.Application.Executors.Pack.Command
{
    public class UpdatePackCommandExecutor : Executor, IRequestHandler<UpdatePackCommand, Result>
    {
        private TripleDbContext _db;
        private IImageService _imageService;

        public UpdatePackCommandExecutor(TripleDbContext db, IImageService imageService)
        {
            _db = db;
            _imageService = imageService;
        }

        public async Task<Result> Handle(UpdatePackCommand request, CancellationToken cancellationToken)
        {
            await request.CommandMustBeValidAsync();

            var pack = await _db.Packs.FirstOrDefaultAsync(x=> x.EntityId == request.PackId);

            if (pack is null)
                return NotFound();

            string days = "";

            request.Days.ForEach(day =>
            {
                days += $"{day}, ";
            });

            pack.CreateOrUpdate(
                request.Code,
                request.Name,
                request.OrganisationId,
                request.Type,
                request.FromDate,
                request.ToDate,
                request.Quantity,
                request.DiscountPrice,
                request.MinRegularPrice,
                request.Description,
                request.EndDate,
                days,
                request.IsRepeatable);

            request.Photos.ForEach(photo =>
            {
                var importResult = _imageService.ImportAsync(photo, "PackPhotos").Result;

                pack.AddPhoto(new Domain.Aggregates.Pack.PackPhoto
                {
                    ImageName = importResult.OriginalFileName,
                    ImagePath = importResult.Path
                });
            });

            _db.Attach(pack);

            _db.Packs.Update(pack);

            await _db.SaveChangesAsync();

            return Succeeded(pack.EntityId);
        }
    }
}
