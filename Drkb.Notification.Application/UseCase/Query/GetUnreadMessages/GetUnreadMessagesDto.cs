namespace Drkb.Notification.Application.UseCase.Query.GetUnreadMessages;

public record GetUnreadMessagesDto
{
    public Guid Id { get; init; }
    public string Type { get; init; } = null!;
    public string PayloadJson { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}