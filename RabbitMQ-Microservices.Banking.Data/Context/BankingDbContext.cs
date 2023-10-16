using Microsoft.EntityFrameworkCore;
using RabbitMQ_MicroServices.Banking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_MicroServices.Banking.Data.Context
{
    public class BankingDbContext: DbContext
    {
        public BankingDbContext(DbContextOptions<BankingDbContext> options): base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
    }
}
