using System;

namespace GigHub.Models
{
    public class Notification
    {
        public int Id { get; private set; }

        public NotificationType NotificationType { get; private set; }

        public DateTime DateTime { get; private set; }

        public DateTime? OriginalDateTime { get; private set; }

        public string OriginalVenue { get; private set; }

        public Gig Gig { get; private set; }

        private Notification()
        { }

        private Notification(NotificationType notificationType, Gig gig)
        {
            Gig = gig ?? throw new ArgumentNullException(nameof(gig));
            NotificationType = notificationType;
            DateTime = DateTime.Now;
        }

        public static Notification GigCanceled(Gig canceledGig)
        {
            return new Notification(NotificationType.GigCanceled, canceledGig);
        }

        public static Notification GigUpdated(string originalVenue, DateTime originalDateTime, Gig updatedGig)
        {
            var result = new Notification(NotificationType.GigCanceled, updatedGig);
            result.OriginalVenue = originalVenue;
            result.OriginalDateTime = originalDateTime;

            return result;
        }
    }
}