using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sender.Models
{

    public class MessageContainer
    {
        public List<Message> Messages { get; }

        public MessageContainer()
        {
            Messages = new List<Message>();
        }

        public void Add(Message message)
        {
            Messages.Add(message);
        }
    }
}
