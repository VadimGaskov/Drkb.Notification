using Microsoft.Extensions.DependencyInjection;

namespace Drkb.Notification.Infrastructure.DI;

public static class UnitOfWorkServiceCollectionExtention
{
    public static IServiceCollection AddUnitOfWork(this IServiceCollection service)
    {
        //Зарегистрировать UoW
        return service;
    }
}