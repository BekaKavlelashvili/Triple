using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Application.Dtos.Chat
{
    public class ChatForAdminDto
    {
        public Guid EntityId { get; set; }

        public string CustomerFirstName { get; set; }

        public string CustomerLastName { get; set; }

        public string SupportFirstName { get; set; }

        public string SupportLastName { get; set; }

        public DateTime? LastMessageTime { get; set; }

        public string LastMessage { get; set; }
    }
}
