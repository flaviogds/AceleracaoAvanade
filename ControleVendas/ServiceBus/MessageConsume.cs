using ControleVendas.Models;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ControleVendas.ServiceBus
{
    public class MessageConsume : BackgroundService
    {
        private readonly ISubscriptionClient _subscription;

        private readonly IServiceBusController _serviceController;

        public MessageConsume(ISubscriptionClient subscription, IServiceBusController serviceController)
        {
            _subscription = subscription;
            _serviceController = serviceController;
        }

        protected override Task ExecuteAsync(CancellationToken stoppinToken)
        {
            _subscription.RegisterMessageHandler(async (message, token) =>
           {
           var product = JsonSerializer.Deserialize<Product>(message.Body);

            await _serviceController.Update(product);

           }, new MessageHandlerOptions(args => Task.CompletedTask));

            return Task.CompletedTask;
        }
    }
}
 