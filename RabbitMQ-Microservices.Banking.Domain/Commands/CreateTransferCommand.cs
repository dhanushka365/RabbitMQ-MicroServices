using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_MicroServices.Banking.Domain.Commands
{
    public class CreateTransferCommand :TransferCommand
    {
        public CreateTransferCommand(int accountFrom, int accountTo, decimal transferAmount) 
        {
            AccountFrom = accountFrom;
            AccountTo = accountTo;
            TransferAmount = transferAmount;
        }

    }
}
