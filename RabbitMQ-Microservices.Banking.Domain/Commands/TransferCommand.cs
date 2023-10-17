using RabbitMQ_MicroServices.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_MicroServices.Banking.Domain.Commands
{
    public class TransferCommand: Command
    {
        public int AccountFrom { get; protected set; }
        public int AccountTo { get; protected set; }
        public decimal TransferAmount { get; protected set; }

    }
}
