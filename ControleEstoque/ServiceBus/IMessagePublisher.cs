using ControleEstoque.Models;
using System.Threading.Tasks;

namespace ControleEstoque.ServiceBus
{
    public interface IMessagePublisher
    {
        public Task SendMessage(Product item, string queue);
    }
}
