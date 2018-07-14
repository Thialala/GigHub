using System.Linq;
using System.Threading.Tasks;
using GigHub.Data;
using GigHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GigHub.Pages.Gigs
{
    [Authorize]
    public class AttendingModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AttendingModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public GigsViewModel GigsViewModel { get; set; }

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            var upcomingGigs = await _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Genre)
                .Include(g => g.Artist)
                .ToListAsync();

            GigsViewModel = new GigsViewModel
            {
                Heading = "Gigs I'm going",
                ShowActions = _signInManager.IsSignedIn(User),
                UpcomingGigs = upcomingGigs
            };
        }
    }
}