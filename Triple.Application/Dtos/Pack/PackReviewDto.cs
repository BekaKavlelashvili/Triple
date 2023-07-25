using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Application.Dtos.Pack
{
    public class PackReviewDto
    {
        public List<PackReviewRatesDto> Rates { get; set; } = new List<PackReviewRatesDto>();

        public decimal OverallRating { get; set; }
    }
}
