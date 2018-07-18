using GigHub.Data.Configuration;
using GigHub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GigHub.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<string>, string>
    {
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new GigConfiguration());
            builder.ApplyConfiguration(new GenreConfiguration());
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new AttendanceConfiguration());
            builder.ApplyConfiguration(new FollowingConfiguration());
            builder.ApplyConfiguration(new NotificationConfiguration());
            builder.ApplyConfiguration(new UserNotificationConfiguration());
        }

        public DbSet<GigHub.Models.ApplicationUser> ApplicationUser { get; set; }
    }
}
