using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace GigHub.Models
{
    public class ApplicationUser : IdentityUser<string>
    {
        public string Name { get; set; }

        public ICollection<ApplicationUser> Followees { get; private set; } = new List<ApplicationUser>();

        public ICollection<ApplicationUser> Followers { get; private set; } = new List<ApplicationUser>();

        public ICollection<UserNotification> UserNotifications { get; private set; } = new List<UserNotification>();

        public void Notify(Notification notification)
        {
            var userNotification = new UserNotification(this, notification);
            UserNotifications.Add(userNotification);
        }
    }
}