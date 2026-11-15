using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            const string queueName = "lab-4-riabokon";
            int counter = 1;

            do
            {
                Thread.Sleep(2000);

                var factory = new ConnectionFactory()
                {
                    HostName = "localhost"
                };

                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queueName,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string message = $"Повідомлення з практичної роботи №{counter}";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: queueName,
                                         basicProperties: null,
                                         body: body);

                    Console.WriteLine($"Надіслано повідомлення №{counter}");
                    counter++;
                }
            } while (true);
        }
    }
}
