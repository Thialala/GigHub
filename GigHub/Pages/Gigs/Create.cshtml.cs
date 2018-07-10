using System.Linq;
using GigHub.Data;
using GigHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GigHub.Pages.Gigs
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        [BindProperty]
        public GigFormViewModel GigViewModel { get; set; }

        public CreateModel(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public void OnGet()
        {
            GigViewModel = new GigFormViewModel { Genres = _dbContext.Genres.ToList() };
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                GigViewModel.Genres = _dbContext.Genres.ToList();
                return Page();
            }

            var gig = new Gig
            {
                ArtistId = _userManager.GetUserId(User),
                DateTime = GigViewModel.GetDateTime(),
                GenreId = GigViewModel.Genre,
                Venue = GigViewModel.Venue
            };

            _dbContext.Add(gig);
            _dbContext.SaveChanges();

            return RedirectToPage("/Index");
        }
    }
}