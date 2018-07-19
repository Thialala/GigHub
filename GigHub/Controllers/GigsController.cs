using System;
using System.Linq;
using GigHub.Data;
using GigHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GigHub.Controllers
{
    [Route("api/gigs")]
    [ApiController]
    [Authorize]
    public class GigsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public GigsController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [HttpDelete("{gigId}")]
        public IActionResult Cancel(int gigId)
        {
            var userId = _userManager.GetUserId(User);
            var gig = _dbContext.Gigs
                                .Include(g => g.Attendances.Select(a => a.Attendee))
                                .SingleOrDefault(g => g.Id == gigId && g.ArtistId == userId && g.DateTime > DateTime.Now);

            if (gig == null)
            {
                return NotFound();
            }

            gig.Cancel();

            _dbContext.Gigs.Update(gig);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}