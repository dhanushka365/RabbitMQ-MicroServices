using RabbitMQ_MicroServices.Domain.Core.Commands;
using RabbitMQ_MicroServices.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_MicroServices.Domain.Core.Bus
{
    public interface IEventBus
    {
        //created concrete business logic in RabbitMQ-MicroServices.Infra.Bus/EventBus.cs
        Task SendCommand<T>(T command) where T: Command;
        void Publish<T>(T @event) where T: Event;

        void Subscribe<T ,TH>()
            where T: Event
            where TH: IEventHandler<T>;

    }
}
