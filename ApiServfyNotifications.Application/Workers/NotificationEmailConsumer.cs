using ApiServfyNotifications.Application.Handlers.Notification.CommandSendEmail.Command;
using ApiServfyNotifications.Application.Services.Contracts;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace ApiServfyNotifications.Application.Workers
{
    public class NotificationEmailConsumerWorker : BackgroundService
    {
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly IEmailNotificationService _emailNotificationService;
        private readonly IConfiguration _configuration;

        public NotificationEmailConsumerWorker(IConsumer<Ignore, string> consumer, IEmailNotificationService emailNotificationService, IConfiguration configuration)
        {
            _consumer = consumer;
            _emailNotificationService = emailNotificationService;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    _consumer.Subscribe(_configuration.GetSection("Kafka:Topic").Value);

                    ConsumeResult<Ignore, string> consumedData = _consumer.Consume(cancellationToken);
                    if (consumedData.IsPartitionEOF)
                    {
                        Console.WriteLine("No data left in kafka to read!");
                    }

                    var requestSendEmail = JsonConvert.DeserializeObject<RequestSendEmail>(consumedData?.Value);

                    if(requestSendEmail == null)
                        _consumer.Commit(consumedData);

                    var resultSendEmail = await _emailNotificationService.SendNotificationEmailAsync(requestSendEmail, cancellationToken);

                    _consumer.Commit(consumedData);
                }
                catch (ConsumeException ex)
                {
                    Console.WriteLine($"Consumer Exception occurred {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception occurred {ex.Message}");
                }
            }
        }
    }
}
