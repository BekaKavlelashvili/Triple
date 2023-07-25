using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.Pack;
using Triple.Application.Queries.Favorite;
using Triple.Application.Shared;
using Triple.Infrastructure;
using Triple.Infrastructure.Persistence;
using Triple.Shared;

namespace Triple.Application.Executors.Favorite.Query
{
    public class SearchFavoriteQueryHandler : IRequestHandler<SearchFavoriteQuery, QueryResultOfList<PackDto>>
    {
        private TripleDbContext _dbContext;
        private readonly IUserService _userService;

        public SearchFavoriteQueryHandler(TripleDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<QueryResultOfList<PackDto>> Handle(SearchFavoriteQuery request, CancellationToken cancellationToken)
        {
            var currentUserEmail = UserIdentity.From(_userService.GetUser()).Email;

            var currentUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == currentUserEmail);

            var favorites = await (from favorite in _dbContext.Favorites.Where(x => x.CustomerId == currentUser.OwnerId)
                                   from pack in _dbContext.Packs.Where(x => x.EntityId == favorite.PackId)
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

            var result = favorites.AsQueryable().FilterAndSort(request.Filters, request.Sortings).ToPaging(request.Page, request.PageSize).OrderBy(x => x.IsPriority).ToList();

            return new QueryResultOfList<PackDto>
            {
                Records = result,
                Total = favorites.Count()
            };
        }
    }
}
