using ControleEstoque.Models;
using System.Threading.Tasks;

namespace ControleEstoque.ServiceBus
{
    public interface IServiceBusController
    {
        public Task Sales(Product item);
    }
}
