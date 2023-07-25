using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Application.Dtos.Chat
{
    public class ChatDetailsForAdminDto
    {
        public Guid ChatId { get; set; }

        public List<MessageForAdminDto> Messages { get; set; } = new List<MessageForAdminDto>();
    }
}
