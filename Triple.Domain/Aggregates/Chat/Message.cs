namespace Triple.Domain.Aggregates.Chat
{
    public class Message
    {
        public int Id { get; set; }

        public int ChatId { get; set; }

        public Guid UserId { get; set; }

        public string? Firstname { get; set; }

        public string? Lastname { get; set; }

        public string? Text { get; set; }

        public bool Seen { get; set; } = false;

        public DateTime Timestamp { get; set; }
    }
}