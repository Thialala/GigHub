using System;

namespace GigHub.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public NotificationType NotificationType { get; set; }

        public DateTime DateTime { get; set; }

        public DateTime? OriginalDateTime { get; set; }

        public string OriginalVenue { get; set; }

        public Gig Gig { get; set; }
    }
}