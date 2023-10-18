using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ_MicroServices.Domain.Core.Bus;
using RabbitMQ_MicroServices.Domain.Core.Commands;
using RabbitMQ_MicroServices.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_MicroServices.Infrastructure.Bus
{
    public sealed class RabbitMQBus : IEventBus
    {
        private readonly IMediator _mediator;
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly List<Type> _eventTypes;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public RabbitMQBus(IMediator mediator, IServiceScopeFactory serviceScopeFactory)
        {
            _mediator = mediator;
            _handlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
            _serviceScopeFactory = serviceScopeFactory;
        }


        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public void Publish<T>(T @event) where T : Event
        {
           var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                //create channel
                using (var channel = connection.CreateModel())
                {
                    //declare queue
                    channel.QueueDeclare(queue: @event.GetType().Name, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    //serialize event
                    var message = Newtonsoft.Json.JsonConvert.SerializeObject(@event);
                    var body = Encoding.UTF8.GetBytes(message);
                    //publish event
                    channel.BasicPublish(exchange: "", routingKey: @event.GetType().Name, basicProperties: null, body: body);
                }
            }
        }

        public void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>
        {
            var eventName = typeof(T).Name;
            var handlerType = typeof(TH);

            if (!_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }

            if (!_handlers.ContainsKey(eventName))
            {
                _handlers.Add(eventName, new List<Type>());
            }

            if (_handlers[eventName].Any(s => s.GetType() == handlerType))
            {
                throw new ArgumentException($"Handler Type {handlerType.Name} already is registered for '{eventName}'", nameof(handlerType));
            }

            _handlers[eventName].Add(handlerType);

            StartBasicConsume<T>();

        }

        private void StartBasicConsume<T>() where T : Event
        {
            var factory = new ConnectionFactory() { HostName = "localhost", DispatchConsumersAsync = true };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var eventName = typeof(T).Name;
            //declare queue
            channel.QueueDeclare(queue: eventName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            //create consumer
            var consumer = new AsyncEventingBasicConsumer(channel);
            //create event handler
            consumer.Received += Consumer_Received;
            //start consuming
            channel.BasicConsume(queue: eventName, autoAck: true, consumer: consumer);

        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            var eventName = @event.RoutingKey;
            var message = Encoding.UTF8.GetString(@event.Body.ToArray());
            try
            {
                await ProcessEvent(eventName, message).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }

        }

        private async Task ProcessEvent(string eventName, string message)
        {
            if(_handlers.ContainsKey(eventName))
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var subscriptions = _handlers[eventName];
                    foreach (var subscription in subscriptions)
                    {
                        //var handler = Activator.CreateInstance(subscription);
                        var handler = scope.ServiceProvider.GetService(subscription);
                        if (handler == null)
                        {
                            continue;
                        }
                        var eventType = _eventTypes.SingleOrDefault(t => t.Name == eventName);
                        var @event = Newtonsoft.Json.JsonConvert.DeserializeObject(message, eventType);
                        var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8601 // Possible null reference assignment.
                        await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { @event });//invoke handle method
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                    }
                }
            }
        }
    }
}
