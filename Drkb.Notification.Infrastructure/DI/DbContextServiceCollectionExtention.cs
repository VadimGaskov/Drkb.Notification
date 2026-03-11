using Drkb.Notification.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Drkb.Notification.Infrastructure.DI;

public static class DbContextServiceCollectionExtention
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<NotificationDbContext>(options =>
        {
            options.UseNpgsql(connectionString, x => x.MigrationsAssembly("Drkb.Notification.Infrastructure"));
        });

        return services;
    }
}