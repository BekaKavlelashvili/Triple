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
    public class GetPackReviewQueryHandler : IRequestHandler<GetPackReviewQuery, PackReviewDto?>
    {
        private TripleDbContext _dbContext;

        public GetPackReviewQueryHandler(TripleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PackReviewDto?> Handle(GetPackReviewQuery request, CancellationToken cancellationToken)
        {
            var packReview = await (from pack in _dbContext.Packs.Where(x => x.EntityId == request.EntityId)
                                    let reviews = from review in pack.Reviews
                                                  select new PackReviewRatesDto
                                                  {
                                                      Comment = review.Comment,
                                                      Rate = review.Rate,
                                                      CustomerId = review.CustomerId,
                                                      PackId = review.PackId
                                                  }
                                    select new PackReviewDto
                                    {
                                        Rates = reviews.ToList(),
                                    }).FirstOrDefaultAsync();

            decimal overallRate = 0;

            packReview.Rates.ForEach(rate =>
            {
                overallRate += rate.Rate;
            });

            overallRate /= packReview.Rates.Count();

            packReview.OverallRating = Math.Round(overallRate, 1, MidpointRounding.ToEven);

            return packReview;
        }
    }
}
