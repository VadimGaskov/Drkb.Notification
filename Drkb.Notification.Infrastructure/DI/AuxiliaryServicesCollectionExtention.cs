using Drkb.Notification.Application.Interfaces;
using Drkb.Notification.Application.Interfaces.Authorization;
using Drkb.Notification.Infrastructure.Services.Authorization;
using Drkb.Notification.Infrastructure.Utils;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace Drkb.Notification.Infrastructure.DI;

public static class AuxiliaryServicesCollectionExtention
{
    public static IServiceCollection AddAuxiliaryServices(this IServiceCollection service)
    {
        service.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
        service.AddScoped<ICurrentUserService, CurrentUserService>();
        return service;
    }
}