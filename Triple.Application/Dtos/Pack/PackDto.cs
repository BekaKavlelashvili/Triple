using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Domain.Aggregates.Pack.Enum;

namespace Triple.Application.Dtos.Pack
{
    public class PackDto
    {
        public string Code { get; set; }

        public Guid OrganisationId { get; set; }

        public PackType Type { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public int Quantity { get; set; }

        public decimal DiscountPrice { get; set; }

        public decimal MinRegularPrice { get; set; }

        public string Description { get; set; }

        public DateTime? EndDate { get; set; }

        public string Days { get; set; }

        public bool IsRepeatable { get; set; }

        public bool IsPriority { get; set; }

        public List<PackPhotoDto> Photos { get; set; }
    }
}
