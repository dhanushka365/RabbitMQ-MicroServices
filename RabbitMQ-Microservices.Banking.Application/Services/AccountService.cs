using RabbitMQ_MicroServices.Banking.Application.Interfaces;
using RabbitMQ_MicroServices.Banking.Application.Models;
using RabbitMQ_MicroServices.Banking.Domain.Commands;
using RabbitMQ_MicroServices.Banking.Domain.Interfaces;
using RabbitMQ_MicroServices.Banking.Domain.Models;
using RabbitMQ_MicroServices.Domain.Core.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_MicroServices.Banking.Application.Services
{
    public class AccountService :IAccountService
    {
        private IAccountRepository _accountRepository;

        private IEventBus _eventBus;

        public AccountService(IAccountRepository accountRepository, IEventBus eventBus)
        {
            _accountRepository = accountRepository;
            _eventBus = eventBus;
        }
        public IEnumerable<Account> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }

        public void Transfer(AccountTransfer accountTransfer)
        {
            var createTransferCommand = new CreateTransferCommand(
               accountTransfer.AccountFrom,
               accountTransfer.AccountTo,
               accountTransfer.TransferAmount
            );
            _eventBus.SendCommand(createTransferCommand); // SendCommand is an extension method
        }
    }
}
