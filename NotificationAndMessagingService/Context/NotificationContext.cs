using Microsoft.EntityFrameworkCore;
using NotificationAndMessagingService.Models;
using System;

namespace NotificationAndMessagingService.Context
{
    public class NotificationContext: DbContext
    {
        public NotificationContext(DbContextOptions<NotificationContext> options) : base(options)
        {

        }
        public DbSet<NotificationHubConnection> NotificationHubConnections { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public void UpdateOrCreateConnection(string username, string newConnectionId)
        {
            var existingConnection = NotificationHubConnections.SingleOrDefault(c => c.Username == username);

            if (existingConnection != null)
            {
                // Update the existing connection
                existingConnection.ConnectionId = newConnectionId;
            }
            else
            {
                // Add a new connection
                NotificationHubConnections.Add(new NotificationHubConnection
                {
                    Username = username,
                    ConnectionId = newConnectionId
                });
            }

            SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notification>()
                .ToTable(tb => tb.HasTrigger("SomeTrigger"));
        }
    }
}
