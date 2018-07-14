using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GigHub.Data;
using GigHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GigHub.Pages.Artists
{
    [Authorize]
    public class FollowingModel : PageModel
    {
        private readonly GigHub.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public FollowingModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<ApplicationUser> Artists { get; set; }

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            Artists = await _context.Followings
                .Where(a => a.FollowerId == userId)
                .Select(f => f.Followee)
                .ToListAsync();
        }
    }
}
