using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.Pack;
using Triple.Application.Queries.Pack;
using Triple.Infrastructure.Persistence;

namespace Triple.Application.Executors.Pack.Query
{
    public class GetPackDetailsQueryHandler : IRequestHandler<GetPackDetailsQuery, PackDto?>
    {
        private TripleDbContext _dbContext;

        public GetPackDetailsQueryHandler(TripleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PackDto?> Handle(GetPackDetailsQuery request, CancellationToken cancellationToken)
        {
            return await (from pack in _dbContext.Packs.Where(x => x.EntityId == request.EntityId)
                              let photos = from photo in pack.Photos
                                           select new PackPhotoDto
                                           {
                                               ImageName = photo.ImageName,
                                               ImagePath = photo.ImagePath
                                           }
                              select new PackDto
                              {
                                  Days = pack.Days,
                                  Description = pack.Description,
                                  DiscountPrice = pack.DiscountPrice,
                                  EndDate = pack.EndDate,
                                  FromDate = pack.FromDate,
                                  ToDate = pack.ToDate,
                                  Code = pack.Code,
                                  IsRepeatable = pack.IsRepeatable,
                                  MinRegularPrice = pack.MinRegularPrice,
                                  OrganisationId = pack.OrganisationId,
                                  Quantity = pack.Quantity,
                                  Type = pack.Type,
                                  IsPriority = pack.IsPriority,
                                  Photos = photos.ToList()
                              }).FirstOrDefaultAsync();
        }
    }
}
