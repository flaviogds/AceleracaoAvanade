using ControleEstoque.Models;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstoque.ServiceBus
{
    public class MessagePublisher : IMessagePublisher
    {
        private readonly ITopicClient _topicClient;

        public MessagePublisher(ITopicClient topicClient)
        {
           _topicClient = topicClient;

        }
        public Task SendMessage(Product item, string queue)
        {
            var ObjectParse = JsonConvert.SerializeObject(item);
            var message = new Message(Encoding.UTF8.GetBytes(ObjectParse))
            {
                ContentType = "application/json"
            };
            message.UserProperties.Add("to", queue);

            return _topicClient.SendAsync(message);
        }
    }
}
