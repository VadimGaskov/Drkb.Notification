using Drkb.Notification.Application.Interfaces;
using Drkb.Notification.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Drkb.Notification.Infrastructure.DI;

public static class LoggerServiceCollectionExtention
{
    public static IServiceCollection AddSerilogLogger(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerService, SerilogLoggerService>();
        return services;
    }
}