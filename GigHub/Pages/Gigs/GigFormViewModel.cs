using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GigHub.Models;

namespace GigHub.Pages.Gigs
{
    public class GigFormViewModel
    {
        public int Id { get; set; }

        public string Heading { get; set; }

        [Required]
        public string Venue { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        public byte Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; } = new List<Genre>();

        public string Action { get; set; }

        public DateTime GetDateTime() => DateTime.Parse($"{Date} {Time}");
    }
}
