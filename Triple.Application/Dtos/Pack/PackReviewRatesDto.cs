namespace Triple.Application.Dtos.Pack
{
    public class PackReviewRatesDto
    {
        public Guid CustomerId { get; set; }

        public int PackId { get; set; }

        public string? Comment { get; set; }

        public int Rate { get; set; }
    }
}