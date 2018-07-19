using GigHub.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigHub.Data.Configuration
{
    public class UserNotificationConfiguration : IEntityTypeConfiguration<UserNotification>
    {
        public void Configure(EntityTypeBuilder<UserNotification> builder)
        {
            builder.HasKey(un => new { un.NotificationId, un.UserId });
            builder.HasOne(un => un.User)
                   .WithMany(u => u.UserNotifications)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
