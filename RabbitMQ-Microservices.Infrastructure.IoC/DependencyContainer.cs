using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ_MicroServices.Banking.Application.Interfaces;
using RabbitMQ_MicroServices.Banking.Application.Services;
using RabbitMQ_MicroServices.Banking.Data.Context;
using RabbitMQ_MicroServices.Banking.Data.Repository;
using RabbitMQ_MicroServices.Banking.Domain.CommandHandlers;
using RabbitMQ_MicroServices.Banking.Domain.Commands;
using RabbitMQ_MicroServices.Banking.Domain.Interfaces;
using RabbitMQ_MicroServices.Domain.Core.Bus;
using RabbitMQ_MicroServices.Infrastructure.Bus;
using RabbitMQ_MicroServices.Transfer.Application.Interfaces;
using RabbitMQ_MicroServices.Transfer.Application.Services;
using RabbitMQ_MicroServices.Transfer.Data.Context;
using RabbitMQ_MicroServices.Transfer.Data.Repository;
using RabbitMQ_MicroServices.Transfer.Domain.EventHandlers;
using RabbitMQ_MicroServices.Transfer.Domain.Events;
using RabbitMQ_MicroServices.Transfer.Domain.Interfaces;

namespace RabbitMQ_Microservices.Infrastructure.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Domain Bus
            services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMQBus(sp.GetService<IMediator>(), scopeFactory);
            });

            //Subscriptions
            services.AddTransient<TransferEventHandler>();
            //Domain Events
            services.AddTransient<IEventHandler<TransferCreatedEvent>, TransferEventHandler>();
            //Domain Banking Commands
            services.AddTransient<IRequestHandler<CreateTransferCommand, bool>,TransferCommandHandler>();
            //Application Services
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ITransferService, TransferService>();
            //Data
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ITransferRepository, TransferRepository>();
            services.AddTransient<BankingDbContext>();
            services.AddTransient<TransferDbContext>();
        }
    }
}
