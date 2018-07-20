using System.Linq;
using System.Threading.Tasks;
using GigHub.Data;
using GigHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GigHub.Pages.Gigs
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public GigFormViewModel GigViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artistId = _userManager.GetUserId(User);
            var gig = await _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .FirstOrDefaultAsync(g => g.Id == id && g.ArtistId == artistId);

            if (gig == null)
            {
                return NotFound();
            }

            GigViewModel = new GigFormViewModel
            {
                Id = gig.Id,
                Heading = "Edit a gig",
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Genre = gig.GenreId,
                Genres = _context.Genres.ToList(),
                Time = gig.DateTime.ToString("HH:mm"),
                Venue = gig.Venue,
                Action = PageContext.ActionDescriptor.ViewEnginePath
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var artistId = _userManager.GetUserId(User);
            var gig = await _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .SingleOrDefaultAsync(g => g.Id == GigViewModel.Id && g.ArtistId == artistId);

            if (gig == null)
            {
                return NotFound();
            }

            gig.Update(GigViewModel.Venue, GigViewModel.GetDateTime(), GigViewModel.Genre);

            _context.Gigs.Update(gig);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GigExists(gig.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToPage("./Mine");
        }

        private bool GigExists(int id)
        {
            return _context.Gigs.Any(e => e.Id == id);
        }
    }
}
