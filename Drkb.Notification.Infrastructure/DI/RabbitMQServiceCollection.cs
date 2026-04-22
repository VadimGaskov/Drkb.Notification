using Drkb.MessageBroker.Masstransit;
using Drkb.Notification.Infrastructure.Data;
using Drkb.Notification.Infrastructure.Services.MessageBroker;
using Drkb.Notification.Infrastructure.Services.MessageBroker.Employment;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Drkb.Notification.Infrastructure.DI;

public static class RabbitMQServiceCollection
{
    public static IServiceCollection AddRabbitMQCollection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDrkbMassTransit<NotificationDbContext>(configuration.GetSection("RabbitMQ"), options =>
        {
            options.DomainName = "notification";

            options.ConfigureRegistration = x =>
            {
                x.AddConsumer<CheckListCompletedConsumer>();
                x.AddConsumer<CheckListCoursesAssignedConsumer>();
                x.AddConsumer<ElementCompletedConsumer>();
            };
        });
        
        return services;
    }
}