using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Application.Dtos.Chat
{
    public class ChatDto
    {
        public Guid EntityId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? LastMessageTime { get; set; }

        public string LastMessage { get; set; }

        public int UnseenMessagesCount { get; set; }
    }
}
