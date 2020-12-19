using ControleVendas.Models;
using ControleVendas.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ControleVendas.ServiceBus
{
    public class ServiceBusController : IServiceBusController
    {
        private readonly IRepositoryService _repository;
        private readonly IMessagePublisher _serviceBus;

        public ServiceBusController(IServiceProvider serviceProvier, IMessagePublisher serviceBus)
        {
            _repository = serviceProvier.GetRequiredService<IRepositoryService>();
            _serviceBus = serviceBus;
        }

        public async Task Add(Product item)
        {
            var product = await _repository.GetAsync(item.Id);

            if (product == null)
            {
                await _repository.AddAsync(item);
            }
        }

        public async Task Update(Product item)
        {
            var product = await _repository.GetAsync(item.Id);

            if (product != null)
            {
                await _repository.UpdateAsync(item.Id, item);
            }
        }

        public async Task Delete(Product item)
        {
            var product = await _repository.GetAsync(item.Id);

            if (product != null)
            {
                await _repository.DeleteAsync(item.Id);
            }
        }

        public async Task<ProductSale> Sales(int id, ProductSale item)
        {
            var product = await _repository.GetAsync(id);

            if(product != null)
            {
                product.Value.Quantidade -= item.Quantidade;
                await _repository.UpdateAsync(id, product.Value);
                await _serviceBus.SendMessage(product.Value, "sale");
            }

            return item;
        }


    }
}
