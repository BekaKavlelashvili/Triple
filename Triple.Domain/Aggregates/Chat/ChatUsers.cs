namespace Triple.Domain.Aggregates.Chat
{
    public class ChatUsers
    {
        public int Id { get; set; }

        public int ChatId { get; set; }

        public Guid UserID { get; set; }

        public string? Firsname { get; set; }

        public string? Lastname { get; set; }
    }
}