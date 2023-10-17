using MediatR;
using RabbitMQ_MicroServices.Banking.Domain.Commands;
using RabbitMQ_MicroServices.Banking.Domain.Events;
using RabbitMQ_MicroServices.Domain.Core.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_MicroServices.Banking.Domain.CommandHandlers
{
    public class TransferCommandHandler : IRequestHandler<CreateTransferCommand , bool>
    {
        private readonly IEventBus _eventBus;

        public TransferCommandHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task<bool> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
        {
            //publish event to RabbitMQ
            _eventBus.Publish(new TransferCreatedEvent(request.AccountFrom, request.AccountTo, request.TransferAmount));
            return Task.FromResult(true);// return true if the transfer is successful

        }

    }
}
