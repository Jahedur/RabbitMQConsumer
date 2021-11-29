using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConsumer
{
    public static class DirectExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            string exchangeName = "demo-direct-exchange";
            string routingKey = "account.init";
            string queueName = "demo-queue";
            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);

            channel.QueueDeclare(
                 queue: queueName,
                 durable: false,
                 autoDelete: false,
                 exclusive: false,
                 arguments: null
                );


            channel.QueueBind(queueName, exchangeName, routingKey);
            channel.BasicQos(0,10, false);
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
