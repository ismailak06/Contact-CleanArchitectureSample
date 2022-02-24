using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Contact.Infrastructure.RabbitMQ
{
    public class Consumer
    {
        private readonly RabbitMQService _rabbitMQService;
        private string _queueName;
        public Consumer(string queueName)
        {
            _rabbitMQService = new RabbitMQService(); //TODO: DI ile üretilmeli
            _queueName = queueName;
        }
    }
}
