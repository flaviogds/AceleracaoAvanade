using ControleVendas.Models;
using System.Threading.Tasks;

namespace ControleVendas.ServiceBus
{
    public interface IServiceBusController
    {
        public Task<ProductSale> Sales(int id, ProductSale item);

        public Task Add(Product item);

        public Task Update(Product item);

        public Task Delete(Product item);

    }
}
