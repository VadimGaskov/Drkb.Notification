using Drkb.Notification.Contract;
using Drkb.Notification.Integration;
using MessageBroker.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Drkb.Notification.Infrastructure.DI;

public static class RabbitMQServiceCollection
{
    public static IServiceCollection AddRabbitMQCollection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRabbitMq(configuration.GetSection("RabbitMQ"), configure =>
        {
            configure.ConfigureConsumer(x =>
            {
                x.Bind<MessageEvent>(
                    exchange: NotificationMetadata.Created.Exchange,
                    queueName: "employment.position.created",
                    routingKey: NotificationMetadata.Created.RoutingKey);
            });
        });
        return services;
    }
}