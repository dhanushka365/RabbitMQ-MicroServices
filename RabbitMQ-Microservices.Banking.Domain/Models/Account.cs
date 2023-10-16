using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_MicroServices.Banking.Domain.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }

    }
}
