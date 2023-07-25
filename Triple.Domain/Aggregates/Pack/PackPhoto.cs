namespace Triple.Domain.Aggregates.Pack
{
    public class PackPhoto
    {
        public int Id { get; set; }

        public int PackId { get; set; }

        public string? ImageName { get; set; }

        public string? ImagePath { get; set; }
    }
}