

using ApiServfyNotifications.Application.Services;
using ApiServfyNotifications.Application.Services.Contracts;
using ApiServfyNotifications.Application.Workers;
using ApiServfyNotifications.Middleware;
using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiServfyNotifications.Application.Modules
{
    public static class ApplicationServicesModule
    {
        public static IServiceCollection AddApplicationServicesModule(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


            //Services
            services.AddTransient<IEmailNotificationService, EmailNotificationService>();

            return services;
        }


        public static IServiceCollection AddNotificationConsumer(this IServiceCollection services, IConfiguration configuration)
        {
            ConsumerConfig config = new ConsumerConfig
            {
                BootstrapServers = configuration.GetSection("Kafka:BootstrapServers").Value,
                SaslMechanism = SaslMechanism.ScramSha256,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslUsername = configuration.GetSection("Kafka:SaslUsername").Value,
                SaslPassword = configuration.GetSection("Kafka:SaslPassword").Value,
                GroupId = configuration.GetSection("Kafka:GroupId").Value,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            services.AddSingleton(new ConsumerBuilder<Ignore, string>(config).Build());
            services.AddHostedService<NotificationEmailConsumerWorker>();


            return services;
        }



    }
}
