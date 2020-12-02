using System;
using System.Text;
using RabbitMQ.Client;

namespace Publisher
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory() {HostName = "localhost"};
            using (var conn = factory.CreateConnection())
            using (var canal = conn.CreateModel())
            {
                canal.QueueDeclare(queue: "primeira.fila", 
                    durable: false, 
                    exclusive: false, 
                    autoDelete: false, 
                    arguments: null);

                string message = "Hello world from RabbitMQ";
                var body = Encoding.UTF8.GetBytes(message);

                canal.BasicPublish(exchange: "",
                routingKey: "primeira.fila", // Bind
                basicProperties: null,
                body: body);

                Console.WriteLine($" => Sent: {message}");
            }    

            Console.WriteLine($"Any key to exit");
            Console.ReadLine();
        }
    }
}
