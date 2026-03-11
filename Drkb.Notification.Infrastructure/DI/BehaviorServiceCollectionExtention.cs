using Drkb.Notification.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Drkb.Notification.Infrastructure.DI;

public static class BehaviorServiceCollectionExtention
{
    public static IServiceCollection AddBehavior(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingAndErrorHandlingBehavior<,>));
        return services;
    }
}