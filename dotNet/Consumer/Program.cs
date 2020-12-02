using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Consumer
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var fabrica = new ConnectionFactory() { HostName = "localhost" };
      
      using (var conn = fabrica.CreateConnection())
      using (var canal = conn.CreateModel())
      {
        canal.QueueDeclare(queue: "primeira.fila", 
                durable: false, 
                exclusive: false, 
                autoDelete: false, 
                arguments: null);

        Console.WriteLine(" => Waiting for messages.");

        var consumer = new EventingBasicConsumer(canal);
        consumer.Received += (model, ea) =>
        {
          var body = ea.Body.ToArray();
          var message = Encoding.UTF8.GetString(body);
          Console.WriteLine($" [x] Received {message}");
        };
        canal.BasicConsume(queue: "primeira.fila", 
                autoAck: true, 
                consumer: consumer);

        Console.WriteLine(" Press any to exit.");
        Console.ReadLine();
      }

    }
  }
}
