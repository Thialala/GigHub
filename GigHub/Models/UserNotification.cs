﻿using System;

namespace GigHub.Models
{
    public class UserNotification
    {
        public string UserId { get; private set; }
        public ApplicationUser User { get; private set; }

        public int NotificationId { get; private set; }
        public Notification Notification { get; private set; }

        public bool IsRead { get; set; }

        private UserNotification() { }

        public UserNotification(ApplicationUser user, Notification notification)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            Notification = notification ?? throw new ArgumentNullException(nameof(notification));
        }
    }
}