using System;
using System.Linq;
using System.Threading.Tasks;
using GigHub.Data;
using GigHub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GigHub.Pages.Gigs
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        public GigsViewModel GigsViewModel { get; set; }

        public async Task OnGetAsync()
        {
            var upcomingGigs = await _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled)
                .ToListAsync();

            GigsViewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                Heading = "Upcoming Gigs",
                ShowActions = _signInManager.IsSignedIn(User)
            };
        }
    }
}
