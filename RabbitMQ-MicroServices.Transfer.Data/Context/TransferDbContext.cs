using Microsoft.EntityFrameworkCore;
using RabbitMQ_MicroServices.Transfer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_MicroServices.Transfer.Data.Context
{
    public class TransferDbContext : DbContext
    {
        public TransferDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TransferLog> TransferLogs { get; set; }

    }
}
