using System;
using System.Linq;
using GigHub.Data;
using GigHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
                .SingleOrDefault(g =>
                g.Id == gigId && g.ArtistId == userId && g.DateTime > DateTime.Now);

            if (gig == null)
            {
                return NotFound();
            }

            gig.IsCanceled = true;
            _dbContext.Gigs.Update(gig);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}