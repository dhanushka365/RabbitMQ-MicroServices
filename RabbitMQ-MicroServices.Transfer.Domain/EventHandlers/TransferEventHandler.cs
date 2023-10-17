using RabbitMQ_MicroServices.Domain.Core.Bus;
using RabbitMQ_MicroServices.Transfer.Domain.Events;
using RabbitMQ_MicroServices.Transfer.Domain.Interfaces;
using RabbitMQ_MicroServices.Transfer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_MicroServices.Transfer.Domain.EventHandlers
{
    public class TransferEventHandler : IEventHandler<TransferCreatedEvent>
    {
        private readonly ITransferRepository _transferRepository;

        public TransferEventHandler(ITransferRepository transferRepository)
        {
            _transferRepository = transferRepository;
        }

        public Task Handle(TransferCreatedEvent @event)
        {
            //add transfer log to database
            _transferRepository.Add(new TransferLog()
            {
                FromAccount = @event.FromAccount,
                ToAccount = @event.ToAccount,
                TransferAmount = @event.TransferAmount
            });

            return Task.CompletedTask;
        }
    }
}
