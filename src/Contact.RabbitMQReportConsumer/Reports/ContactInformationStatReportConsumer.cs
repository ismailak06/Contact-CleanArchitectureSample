using Contact.Application.Document.Command;
using Contact.Application.DocumentLog.Commands;
using Contact.Infrastructure.RabbitMQ;
using Contact.Infrastructure.Utilities.IoC;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Contact.RabbitMQReportConsumer.Reports
{
    public class ContactInformationStatReportConsumer
    {
        private readonly RabbitMQService _rabbitMQService;
        private string _queueName = "ContactInformationStatReportQueue";
        IMediator _mediator;
        public ContactInformationStatReportConsumer()
        {
            _rabbitMQService = new RabbitMQService();
            _mediator = ServiceLocator.ServiceProvider.GetService<IMediator>();
        }

        public void Consumer()
        {
            Console.WriteLine("Consumer started.");
            string message = string.Empty;
            using (var connection = _rabbitMQService.GetRabbitMQConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    //channel.QueueDeclare(queue: "ContactInformationStatReportQueue",
                    //                     durable: false,
                    //                     exclusive: false,
                    //                     autoDelete: false,
                    //                     arguments: null);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += async (model, ea) =>
                    {
                        Thread.Sleep(1000);
                        var body = ea.Body.ToArray();
                        message = Encoding.UTF8.GetString(body);

                        int documentLogId = int.Parse(message);
                        var result = await _mediator.Send(new CreateContactInformationStatReportCommand
                        {
                            DocumentLogId = documentLogId,
                        });
                        Console.WriteLine($"[x] Document processed : {result.DocumentLogId}");

                    };
                    channel.BasicConsume(_queueName, true, consumer);
                    Console.ReadLine();
                }
            }
        }
    }
}
