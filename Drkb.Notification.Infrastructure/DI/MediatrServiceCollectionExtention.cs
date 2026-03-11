using Drkb.Notification.Application;
using Drkb.Notification.Application.UseCase.Command;
using Microsoft.Extensions.DependencyInjection;

namespace Drkb.Notification.Infrastructure.DI;

public static class MediatrServiceCollectionExtention
{
    public static IServiceCollection AddMediatr(this IServiceCollection services)
    {
        services.AddMediatR(msc => msc.RegisterServicesFromAssemblies(typeof(ApplicationAssemblyMarker).Assembly));
        return services;
    }
}