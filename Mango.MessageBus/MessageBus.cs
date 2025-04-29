using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mango.MessageBus
{
    public class MessageBus : IMessageBus
    {

        private string connectionString = "Endpoint=sb://mangoweb.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=HjoslS58pPHtAULb0tay/jx4Ys0+MO5/R+ASbCcFTG0=";

        public async Task PublishMessage(object message, string topic_queue_Name)
        {
            var client = new ServiceBusClient(connectionString);
            try
            {
                ServiceBusSender sender = client.CreateSender(topic_queue_Name);

                var jsonMessage = JsonConvert.SerializeObject(message);
                ServiceBusMessage finalMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
                {
                    CorrelationId = Guid.NewGuid().ToString(),
                };

                await sender.SendMessageAsync(finalMessage);
            }
            finally
            {
                // 確保在支援 async dispose 的情況下正確釋放資源
                if (client is IAsyncDisposable asyncDisposable)
                {
                    await asyncDisposable.DisposeAsync();
                }
                else
                {
                    client.DisposeAsync(); // fallback
                }
            }
        }

    }
}