using Microsoft.EntityFrameworkCore;
using RabbitMQ_MicroServices.Transfer.Domain.Models;

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
