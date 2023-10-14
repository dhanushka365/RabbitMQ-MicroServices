using RabbitMQ_MicroServices.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_MicroServices.Domain.Core.Commands
{
    public abstract class Command : Message
    {
        public DateTime Timestamp { get; protected set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}
