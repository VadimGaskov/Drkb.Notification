using Drkb.Notification.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Drkb.Notification.Infrastructure.Data;

public class NotificationDbContext: DbContext
{
    public DbSet<Message> Messages { get; set; }
    
    public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options)
    {
        
    }
}