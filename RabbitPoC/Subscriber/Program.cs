using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Common;

namespace Subscriber
{
    public class Program
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static QueueingBasicConsumer _consumer;

        private const string ExchangeName = "PS_Exchange";

        static void Main(string[] args)
        {
            Receive();
            Console.ReadLine();
        }

        private static string DeclareAndBindQueueToExchange(IModel channel)
        {
            channel.ExchangeDeclare(ExchangeName, "fanout");
            //The fanout exchange broadcasts all the messages it receives to all the queues it knows.

            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queueName, ExchangeName, "");
            _consumer = new QueueingBasicConsumer(channel);
            return queueName;
        }

        private static void Receive()
        {
            _factory = new ConnectionFactory
                            {
                                HostName = "localhost",
                                UserName = "guest",
                                Password = "guest"
                            };

            using (_connection = _factory.CreateConnection())
            {
                using (var channel = _connection.CreateModel())
                {
                    var queueName = DeclareAndBindQueueToExchange(channel);
                    channel.BasicConsume(queueName, true, _consumer);

                    while (true)
                    {
                        var dq = _consumer.Queue.Dequeue();
                        var msg = (Payment)dq.Body.DeSerialize(typeof(Payment));

                        Console.WriteLine(String.Format("Payment Processed {0} : {1}", msg.CardNumber, msg.AmountToPay));
                    }
                }
            }
        }
    }
}
