﻿using System;

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
    }
}
