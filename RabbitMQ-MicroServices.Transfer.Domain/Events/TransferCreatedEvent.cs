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
        public int FromAccount { get; protected set; }
        public int ToAccount { get; protected set; }
        public decimal TransferAmount { get; protected set; }

        public TransferCreatedEvent(int fromAccount, int toAccount, decimal transferAmount)
        {
            FromAccount = fromAccount;
            ToAccount = toAccount;
            TransferAmount = transferAmount;
        }
    }
}
