using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.Pack;
using Triple.Application.Queries.Pack;
using Triple.Application.Shared;
using Triple.Infrastructure.Persistence;
using Triple.Shared;

namespace Triple.Application.Executors.Pack.Query
{
    public class SearchPackQueryHandler : IRequestHandler<SearchPackQuery, QueryResultOfList<PackDto>>
    {
        private TripleDbContext _dbContext;

        public SearchPackQueryHandler(TripleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<QueryResultOfList<PackDto>> Handle(SearchPackQuery request, CancellationToken cancellationToken)
        {
            var packs = await (from pack in _dbContext.Packs
                               let photos = from photo in pack.Photos
                                            select new PackPhotoDto
                                            {
                                                ImageName = photo.ImageName,
                                                ImagePath = photo.ImagePath
                                            }
                               select new PackDto()
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
                               }).ToListAsync();

            var packsList = new List<PackDto>();

            packs.ForEach(pack =>
            {
                if (pack.IsRepeatable)
                {
                    var days = pack.Days.Split(',');

                    if (pack.EndDate >= DateTime.Now)
                    {
                        for (int i = 0; i <= days.Length - 1; i++)
                        {
                            if (days[i] != DateTime.Now.DayOfWeek.ToString())
                            {
                                if (!packsList.Contains(pack))
                                    packsList.Add(pack);
                            }
                        }
                    }
                }
            });

            if (packsList.Count >= 1)
            {
                packsList.ForEach(pack =>
                {
                    packs = packs.Where(x => x.EndDate >= DateTime.Now && pack.Code != x.Code).ToList();
                });
            }
            else
            {
                packs.Clear();
            }

            packs.AddRange(packsList);

            var result = packs.AsQueryable().FilterAndSort(request.Filters, request.Sortings).ToPaging(request.Page, request.PageSize).OrderBy(x=> x.IsPriority).ToList();

            return new QueryResultOfList<PackDto>
            {
                Records = result,
                Total = packs.Count()
            };
        }
    }
}
