using ControleVendas.Models;
using System.Threading.Tasks;

namespace ControleVendas.ServiceBus
{
    public interface IServiceBusController
    {
        public Task Sales(int id, ProductSale item);

        public Task Update(Product item);
    }
}
