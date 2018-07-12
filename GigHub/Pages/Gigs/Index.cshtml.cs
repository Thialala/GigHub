using GigHub.Data;
using GigHub.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigHub.Pages.Gigs
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Gig> Gigs { get; set; }

        public async Task OnGetAsync()
        {
            Gigs = await _context.Gigs
                .Include(g => g.Artist)
                .Where(g => g.DateTime > DateTime.Now)
                .ToListAsync();
        }
    }
}
