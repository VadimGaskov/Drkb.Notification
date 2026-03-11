using Drkb.Notification.Application.Interfaces.QueryObjects;

namespace Drkb.Notification.Application.UseCase.Query.GetUnreadMessages;

public interface IUnreadMessagesQuery: IQueryObject<GetUnreadMessagesQuery, List<GetUnreadMessagesDto>>
{
    
}