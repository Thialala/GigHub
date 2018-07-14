using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Pages.Gigs
{
    public class GigsViewModel
    {
        public IList<Gig> UpcomingGigs { get; set; } = new List<Gig>();
        public string Heading { get; set; }
        public bool ShowActions { get; set; }
    }
}
