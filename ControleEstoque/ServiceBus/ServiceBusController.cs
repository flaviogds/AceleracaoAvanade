using ControleEstoque.Models;
using ControleEstoque.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ControleEstoque.ServiceBus
{
    public class ServiceBusController : IServiceBusController
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceBusController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Sales(Product item)
        {
            using(var scope = _serviceProvider.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IProductRepository>();

                await repository.UpdateAsync(item.Id, item);
            }
        }
    }
}
