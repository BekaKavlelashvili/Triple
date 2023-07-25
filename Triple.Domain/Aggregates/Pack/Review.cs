namespace Triple.Domain.Aggregates.Pack
{
    public class Review
    {
        public int Id { get; set; }

        public int PackId { get; set; }

        public Guid CustomerId { get; set; }

        public int Rate { get; set; }

        public string? Comment { get; set; }
    }
}