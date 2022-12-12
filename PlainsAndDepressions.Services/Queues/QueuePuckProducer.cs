using RabbitMQ.Client;
using System.Text;

namespace PlainsAndDepressions.Services.Queues
{
    public static class QueuePuckProducer
    {
        private const string _dipressionPacks = "DipressionPacks";

        public static void Publich(IModel channel, string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: string.Empty,
                           routingKey: _dipressionPacks,
                           basicProperties: null,
                           body: body);
        }
    }
}
