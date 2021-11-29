using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConsumer
{
    public class QueueConsumer
    {
        public static void Consume(IModel channel)
        {
            const string queueName = "testqueue";

            channel.QueueDeclare(
                 queue: queueName,
                 durable: false,
                 autoDelete: false,
                 exclusive: false,
                 arguments: null
                );


            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += Consumer_Received;

            channel.BasicConsume(queueName, true, consumer);

            Console.WriteLine("Consumer started");
        }

        private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var body = System.Text.Encoding.UTF8.GetString(e.Body.ToArray());
            Console.WriteLine(body);
        }
    }
}
