using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQConsumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest",
                RequestedConnectionTimeout = new System.TimeSpan(0, 0, 3)
            };

            var rabbitConnection = connectionFactory.CreateConnection();
            var channel = rabbitConnection.CreateModel();

            //QueueConsumer.Consume(channel);
            DirectExchangeConsumer.Consume(channel);
            Console.ReadLine();
        }

        private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var body = System.Text.Encoding.UTF8.GetString(e.Body.ToArray());
            Console.WriteLine(body);
        }
    }
}
