using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Common;

namespace Consumer
{
    public class Program
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private const string QueueName = "WorkerQueue_Queue";

        static void Main(string[] args)
        {
            Receive();
            Console.ReadLine();
        }

        public static void Receive()
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
                    channel.QueueDeclare(QueueName, true, false, false, null);
                    channel.BasicQos(0, 1, false);

                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(QueueName, false, consumer);

                    while (true)
                    {
                        //this infinite while loop wil cause the program to 
                        //always listen in on the queue. If something comes in, 
                        //it'll process and acknowledge it. 

                        var dq = consumer.Queue.Dequeue();
                        var msg = (Payment)dq.Body.DeSerialize(typeof(Payment));

                        channel.BasicAck(dq.DeliveryTag, false);

                        Console.WriteLine(String.Format("Payment Processed {0} : {1}",
                                    msg.CardNumber,
                                    msg.AmountToPay));
                    }
                }
            }
        }
    }
}
