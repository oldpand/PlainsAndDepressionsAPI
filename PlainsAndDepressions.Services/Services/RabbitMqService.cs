using PlainsAndDepressions.Services.Queues;
using RabbitMQ.Client;
using System.Text.Json;
using System.Threading.Channels;

namespace PlainsAndDepressions.Services.Services
{
    public class RabbitMqService : IRabbitMqService, IDisposable
    {
        private const string _dipressionPacks = "DipressionPacks";

        private readonly IModel _channel = null!;

        private readonly IConnection _connection = null!;

        public RabbitMqService()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: _dipressionPacks,
               durable: false,
               exclusive: false,
               autoDelete: false,
               arguments: null);
        }

        public void SendMessage(object obj)
        {
            var message = JsonSerializer.Serialize(obj);
            SendMessage(message);
        }

        public void SendMessage(string message)
        {
            QueuePuckProducer.Publich(_channel, message);
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
            _channel.Dispose();
            _connection.Dispose();
        }
    }
}
