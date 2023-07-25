using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Domain.Shared;

namespace Triple.Domain.Aggregates.Chat
{
    public class Chat : AggregateRoot
    {
        public Chat()
        {

        }

        public Chat(Guid EntityId) : base(EntityId)
        {

        }

        public ICollection<Message> Messages { get; set; } = new HashSet<Message>();

        public ICollection<ChatUsers> Users { get; set; } = new HashSet<ChatUsers>();

        public DateTime? LastOpenTime { get; set; }

        public Guid? OrderId { get; set; }

        public string TemplateNote { get; set; }

        public void AddUsers(ChatUsers user)
        {
            Users.Add(user);
        }

        public void AddMessage(Message message)
        {
            Messages.Add(message);
        }
    }
}
