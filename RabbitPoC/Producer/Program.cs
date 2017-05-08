using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Common;

namespace Producer
{
    public class Program
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _model;

        private const string QueueName = "WorkerQueue_Queue";

        static void Main(string[] args)
        {
            List <Payment> payments = PaymentHelper.GetDummyPayments();
            CreateConnection();

            foreach (Payment p in payments)
            {
                SendMessage(p);
            }

            Console.ReadLine();
        }

        private static void CreateConnection()
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
            Console.WriteLine(String.Format("Payment Sent {0} : {1}", message.CardNumber, message.AmountToPay));
        }
    }
}
