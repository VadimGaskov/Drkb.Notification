using Drkb.ResultObjects;
using MediatR;

namespace Drkb.Notification.Application.UseCase.Command.MarkMessageByIdAsRead;

public class MarkMessageByIdAsReadHandler: IRequestHandler<MarkMessageByIdAsReadCommand, Result>
{
    private readonly IMarkMessageByIdAsReadDataProvider _markMessage;

    public MarkMessageByIdAsReadHandler(IMarkMessageByIdAsReadDataProvider markMessage)
    {
        _markMessage = markMessage;
    }

    public async Task<Result> Handle(MarkMessageByIdAsReadCommand request, CancellationToken cancellationToken)
    {
        await _markMessage.ExecuteAsync(request, cancellationToken);
        return Result.Success();
    }
}