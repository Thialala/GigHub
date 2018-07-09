﻿using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Pages.Gigs
{
    public class GigFormViewModel
    {
        public string Venue { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int Genre { get; set; }
        public IEnumerable<Genre> Genres { get; set; } = new List<Genre>();
    }
}
