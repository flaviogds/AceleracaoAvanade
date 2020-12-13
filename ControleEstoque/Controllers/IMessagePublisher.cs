using System;
using System.Text.Json;
using ControleEstoque.Models;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace ControleEstoque.Controllers
{
    public class IMessagePublisher
    {
        public void Publisher (Produto produto, IConfiguration configuration)
        {
            var serviceBusClient = new TopicClient(configuration["ConnectionString"], configuration["EntityPath"]);

            var message = new Message(JsonSerializer.SerializeToUtf8Bytes(produto));

            message.ContentType = "application/json";
            message.UserProperties.Add("CorrelationId", Guid.NewGuid().ToString());

            serviceBusClient.SendAsync(message);

        }
    }
}
