using Drkb.Notification.Contract;
using Drkb.Notification.Infrastructure.Services.MessageBroker;
using MessageBroker.Abstractions.Interfaces.Consumer;
using Microsoft.Extensions.DependencyInjection;

namespace Drkb.Notification.Infrastructure.DI;

public static class EventHandlerServiceCollection
{
    public static IServiceCollection AddEventHandlerServices(this IServiceCollection services)
    {
        services.AddScoped<IEventHandler<MessageEvent>, MessageEventHandler>();
        return services;
    }
}