using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Application.Dtos.Chat
{
    public class MessageDto
    {
        public Guid UserId { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Text { get; set; }

        public DateTime Timestamp { get; set; }

        public bool IsCurrentUser { get; set; }

        public bool Seen { get; set; }
    }
}
