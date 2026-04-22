using Drkb.Notification.Domain.Entity;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Drkb.Notification.Infrastructure.Data;

public class NotificationDbContext : DbContext
{
    public DbSet<Message> Messages { get; set; }

    public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }

}