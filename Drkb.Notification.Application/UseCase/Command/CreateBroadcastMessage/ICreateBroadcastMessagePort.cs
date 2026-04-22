using Drkb.Notification.Application.Interfaces.DataProvider;
using Drkb.Notification.Domain.Entity;

namespace Drkb.Notification.Application.UseCase.Command.CreateBroadcastMessage;

public interface ICreateBroadcastMessagePort: IPortMarker, 
    IAddPort<List<Message>>
{
    
}