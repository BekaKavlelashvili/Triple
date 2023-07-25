using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Domain.Aggregates.Pack.Enum;
using Triple.Domain.Shared;

namespace Triple.Domain.Aggregates.Pack
{
    public class Pack : AggregateRoot
    {
        public Pack()
        {

        }

        public Pack(Guid EntityId) : base(EntityId)
        {

        }

        public string Code { get; set; }

        public string Name { get; set; }

        public Guid OrganisationId { get; set; }

        public PackType Type { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int Quantity { get; set; }

        public decimal DiscountPrice { get; set; }

        public decimal MinRegularPrice { get; set; }

        public string Description { get; set; }

        public DateTime EndDate { get; set; }

        public string Days { get; set; }

        public bool IsRepeatable { get; set; }

        public bool IsPriority { get; set; }

        public ICollection<PackPhoto> Photos { get; set; } = new HashSet<PackPhoto>();

        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

        public void AddReview(Review review)
        {
            Reviews.Add(review);
        }

        public void CreateOrUpdate(
            string code,
            string name,
            Guid organisationId,
            PackType type,
            DateTime fromDate,
            DateTime toDate,
            int quantity,
            decimal discountPrice,
            decimal minRegularPrice,
            string description,
            DateTime endDate,
            string days,
            bool isRepeatable)
        {
            Code = code;
            Name = name;
            OrganisationId = organisationId;
            Type = type;
            FromDate = fromDate;
            ToDate = toDate;
            Quantity = quantity;
            DiscountPrice = discountPrice;
            MinRegularPrice = minRegularPrice;
            Description = description;
            EndDate = endDate;
            Days = days;
            IsRepeatable = isRepeatable;
        }

        public void AddPhoto(PackPhoto photo)
        {
            Photos.Add(photo);
        }
    }
}
