using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Common;

namespace StandardQueue
{
    public class Program
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _model;

        private const string QueueName = "SQ_Queue";

        public static void Main(string[] args)
        {
            List<Payment> payments = GetDummyPayments();
            CreateQueue();
            
            foreach (Payment p in payments)
            {
                SendMessage(p);
            }

            Receive();

            Console.ReadLine();
        }

        private static List<Payment> GetDummyPayments()
        {
            List<Payment> ret = new List<Payment>();

            Payment payment1 = new Payment { AmountToPay = 25.0m, CardNumber = "1234123412341234", Name = "Manu Ginobili" };
            Payment payment2 = new Payment { AmountToPay = 5.0m, CardNumber = "1234123412341234", Name = "Manu Ginobili" };
            Payment payment3 = new Payment { AmountToPay = 2.0m, CardNumber = "1234123412341234", Name = "Manu Ginobili" };
            Payment payment4 = new Payment { AmountToPay = 17.0m, CardNumber = "1234123412341234", Name = "Manu Ginobili" };
            Payment payment5 = new Payment { AmountToPay = 300.0m, CardNumber = "1234123412341234", Name = "Manu Ginobili" };
            Payment payment6 = new Payment { AmountToPay = 350.0m, CardNumber = "1234123412341234", Name = "Manu Ginobili" };
            Payment payment7 = new Payment { AmountToPay = 295.0m, CardNumber = "1234123412341234", Name = "Manu Ginobili" };
            Payment payment8 = new Payment { AmountToPay = 5625.0m, CardNumber = "1234123412341234", Name = "Manu Ginobili" };
            Payment payment9 = new Payment { AmountToPay = 5.0m, CardNumber = "1234123412341234", Name = "Manu Ginobili" };
            Payment payment10 = new Payment { AmountToPay = 12.0m, CardNumber = "1234123412341234", Name = "Manu Ginobili" };

            ret.Add(payment1);
            ret.Add(payment2);
            ret.Add(payment3);
            ret.Add(payment4);
            ret.Add(payment5);
            ret.Add(payment6);
            ret.Add(payment7);
            ret.Add(payment8);
            ret.Add(payment9);
            ret.Add(payment10);

            return ret;
        }

        private static void CreateQueue()
        {
            _factory = new ConnectionFactory
                        {
                            HostName = "localhost",
                            UserName = "guest",
                            Password = "guest"
                        };

            _connection = _factory.CreateConnection();
            _model = _connection.CreateModel();

            _model.QueueDeclare(QueueName, true, false, false, null);            
        }

        private static void SendMessage(Payment message)
        {
            _model.BasicPublish("", QueueName, null, message.Serialize());
            Console.WriteLine(String.Format("Payment Sent: {0} : {1} : {2}",
                                        message.CardNumber,
                                        message.AmountToPay,
                                        message.Name));
        }

        private static uint GetMessageCount(IModel channel, string queueName)
        {
            var results = channel.QueueDeclare(queueName, true, false, false, null);
            return results.MessageCount;
        }

        public static void Receive()
        {
            var consumer = new QueueingBasicConsumer(_model);
            var msgCount = GetMessageCount(_model, QueueName);

            _model.BasicConsume(QueueName, true, consumer);
            var count = 0;

            while (count < msgCount)
            {
                var msg = (Payment)consumer.Queue.Dequeue().Body.DeSerialize(typeof(Payment));
                Console.WriteLine(String.Format("===== Received {0} : {1} : {2}",
                                        msg.CardNumber,
                                        msg.AmountToPay,
                                        msg.Name));
                count++;
            }
        }

    }
}
