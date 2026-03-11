using Drkb.Notification.Application.Interfaces.DataProvider;
using Drkb.Notification.Domain.Entity;

namespace Drkb.Notification.Application.UseCase.Command.CreateMessage;

public interface ICreateMessageDataProvider: IDataProviderMarker, 
    IAddDataProvider<List<Message>>,
    IAddDataProvider<Message>
{
    
}