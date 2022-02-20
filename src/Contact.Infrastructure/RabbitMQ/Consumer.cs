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

        public T GetMessage<T>()
        {
            string message = string.Empty;
            using (var connection = _rabbitMQService.GetRabbitMQConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        message = Encoding.UTF8.GetString(body);
                    };
                    channel.BasicConsume(_queueName, true, consumer);
                }
            }
            return JsonConvert.DeserializeObject<T>(message);
        }
    }
}
