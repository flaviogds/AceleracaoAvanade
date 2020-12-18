using ControleVendas.Models;
using System.Threading.Tasks;

namespace ControleVendas.ServiceBus
{
    public interface IMessagePublisher
    {
        public Task SendMessage(Product item, string queue);
    }
}
