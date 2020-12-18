using ControleEstoque.Models;
using ControleEstoque.Service;
using System.Threading.Tasks;

namespace ControleEstoque.ServiceBus
{
    public class ServiceBusController : IServiceBusController
    {
        private readonly IRepositoryService _repository;

        public ServiceBusController(IRepositoryService repository)
        {
            _repository = repository;
        }

        public async Task Sales(Product item)
        {
            await _repository.UpdateAsync(item.Id, item);
        }
    }
}
