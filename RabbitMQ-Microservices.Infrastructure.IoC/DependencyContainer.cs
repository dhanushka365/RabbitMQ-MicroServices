using Microsoft.Extensions.DependencyInjection;
using RabbitMQ_MicroServices.Banking.Application.Interfaces;
using RabbitMQ_MicroServices.Banking.Application.Services;
using RabbitMQ_MicroServices.Banking.Data.Context;
using RabbitMQ_MicroServices.Banking.Data.Repository;
using RabbitMQ_MicroServices.Banking.Domain.Interfaces;
using RabbitMQ_MicroServices.Domain.Core.Bus;
using RabbitMQ_MicroServices.Infrastructure.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_Microservices.Infrastructure.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Domain Bus
            services.AddTransient<IEventBus, RabbitMQBus>();
            //Domain Events
            //services.AddTransient<IEventHandler<TransferCreatedEvent>, TransferEventHandler>();
            //Application Services
            services.AddTransient<IAccountService, AccountService>();
            //Data
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<BankingDbContext>();
        }
    }
}
