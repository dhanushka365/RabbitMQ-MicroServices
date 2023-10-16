using RabbitMQ_MicroServices.Banking.Data.Context;
using RabbitMQ_MicroServices.Banking.Domain.Interfaces;
using RabbitMQ_MicroServices.Banking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_MicroServices.Banking.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private BankingDbContext _context;

        public AccountRepository(BankingDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Account> GetAccounts()
        {
           return _context.Accounts;
        }

    }
}
