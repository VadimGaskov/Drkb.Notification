using Drkb.Notification.Application.Interfaces.DataProvider;
using Microsoft.Extensions.DependencyInjection;

namespace Drkb.Notification.Infrastructure.DI;

public static class DataProviderServiceCollectionExtention
{
    public static IServiceCollection AddDataProviderServices(this IServiceCollection service)
    {
        service.Scan(scan => scan
            .FromAssemblyOf<InfrastructureAssemblyMarker>()
            .AddClasses(classes => classes.AssignableTo<IPortMarker>())
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );
        return service;
    }
}