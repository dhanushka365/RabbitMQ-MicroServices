using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_MicroServices.Domain.Core.Events
{
    public abstract class Event
    {
        public DateTime Timestamp { get; protected set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
