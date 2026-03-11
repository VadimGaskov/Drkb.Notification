namespace Drkb.Notification.Application.Interfaces.DataProvider;

public interface IGetByIdDataProvider<TResponse>: IDataProviderMarker
{
    public Task<TResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}