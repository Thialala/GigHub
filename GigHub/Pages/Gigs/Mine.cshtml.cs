using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GigHub.Data;
using GigHub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GigHub.Pages.Gigs
{
    public class MineModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MineModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Gig> Gigs { get; set; }

        public async Task OnGetAsync()
        {
            var artistId = _userManager.GetUserId(User);
            Gigs = await _context.Gigs
                .Where(g => g.ArtistId == artistId && g.DateTime > DateTime.Now)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToListAsync();
        }
    }
}
