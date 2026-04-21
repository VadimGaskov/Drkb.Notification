using Drkb.MessageBroker.Masstransit;
using Drkb.Notification.Infrastructure.Data;
using Drkb.Notification.Infrastructure.Services.MessageBroker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Drkb.Notification.Infrastructure.DI;

public static class RabbitMQServiceCollection
{
    public static IServiceCollection AddRabbitMQCollection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDrkbMassTransit<NotificationDbContext>(configuration.GetSection("RabbitMQ"), options =>
        {
            options.DomainName = "Notification";

            options.ConfigureRegistration = x =>
            {
                x.AddConsumer<MessageConsumer>();
            };
        });
        
        return services;
    }
}