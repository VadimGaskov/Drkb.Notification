namespace Drkb.Notification.Domain.Entity;

public class Message: BaseEntity
{
    public Guid? UserId { get; set; }
    public string Type { get; set; } = null!;
    public string PayloadJson { get; set; } = null!;
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
}