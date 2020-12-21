using ControleEstoque.Models;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ControleEstoque.ServiceBus
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
            _subscription.RegisterMessageHandler(async (message, token) => {
                var product = JsonConvert.DeserializeObject<Product>(Encoding.UTF8.GetString(message.Body));

                await _serviceController.Sales(product);

            }, new MessageHandlerOptions(args => Task.CompletedTask));

            return Task.CompletedTask;
        }
    }
}
 