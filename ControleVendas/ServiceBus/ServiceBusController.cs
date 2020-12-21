using ControleVendas.Models;
using ControleVendas.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ControleVendas.ServiceBus
{
    public class ServiceBusController : IServiceBusController
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly IMessagePublisher _serviceBus;

        public ServiceBusController(IServiceProvider serviceProvier, IMessagePublisher serviceBus)
        {
            _serviceProvider = serviceProvier;
            _serviceBus = serviceBus;
        }

        public async Task Add(Product item)
        {
            using (var scopeProvider = _serviceProvider.CreateScope())
            {
                var repository = scopeProvider.ServiceProvider.GetRequiredService<IProductRepository>();

                await repository.AddAsync(item);
            }
        }

        public async Task Update(Product item)
        {
            using (var scopeProvider = _serviceProvider.CreateScope())
            {
                var repository = scopeProvider.ServiceProvider.GetRequiredService<IProductRepository>();

                await repository.UpdateAsync(item.Id, item);
            }
        }

        public async Task Delete(Product item)
        {
            using (var scopeProvider = _serviceProvider.CreateScope())
            {
                var repository = scopeProvider.ServiceProvider.GetRequiredService<IProductRepository>();

                await repository.DeleteAsync(item.Id);
            }
        }

        public async Task<ProductSale> Sales(int id, ProductSale item)
        {
            using (var scopeProvider = _serviceProvider.CreateScope())
            {
                var repository = scopeProvider.ServiceProvider.GetRequiredService<IProductRepository>();

                var product = await repository.GetAsync(id);

                if (product != null)
                {
                    product.Value.Quantidade -= item.Quantidade;
                    await repository.UpdateAsync(id, product.Value);
                    await _serviceBus.SendMessage(product.Value, "sale");
                }

                return item;
            }
        }


    }
}
