using ControleVendas.Models;
using ControleVendas.Service;
using System.Threading.Tasks;

namespace ControleVendas.ServiceBus
{
    public class ServiceBusController : IServiceBusController
    {
        private readonly IRepositoryService _repository;
        private readonly IMessagePublisher _serviceBus;

        public ServiceBusController(IRepositoryService repository, IMessagePublisher serviceBus)
        {
            _repository = repository;
            _serviceBus = serviceBus;
        }

        public async Task Sales(int id, ProductSale item)
        {
            var product = await _repository.GetAsync(id);

            if(product != null)
            {
                product.Value.Quantidade -= item.Quantidade;
                await _repository.UpdateAsync(id, product.Value);
                await _serviceBus.SendMessage(product.Value, "sale");
            }
        }

        public async Task Update(Product item)
        {
            var product = await _repository.GetAsync(item.Id);

            if(product == null)
            {
                await _repository.AddAsync(item);
            }
            if(product != null)
            {
                await _repository.UpdateAsync(item.Id, item);
            }
        }
    }
}
