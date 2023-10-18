
using RabbitMQ_MicroServices.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_MicroServices.Transfer.Domain.Events
{
    public class TransferCreatedEvent:Event
    {
        public int AccountFrom { get; protected set; }
        public int AccountTo { get; protected set; }
        public decimal TransferAmount { get; protected set; }

        public TransferCreatedEvent(int accountFrom, int accountTo, decimal transferAmount)
        {
            AccountFrom = accountFrom;
            AccountTo = accountTo;
            TransferAmount = transferAmount;
        }
    }
}
