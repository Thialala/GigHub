using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public string ArtistId { get; set; }

        public ApplicationUser Artist { get; set; }

        public DateTime DateTime { get; set; }

        public string Venue { get; set; }

        public byte GenreId { get; set; }

        public Genre Genre { get; set; }

        public bool IsCanceled { get; set; }

        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

        public void Cancel()
        {
            IsCanceled = true;

            var notification = new Notification(NotificationType.GigCanceled, this);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }
    }
}
