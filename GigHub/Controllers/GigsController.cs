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

            var notification = new Notification
            {
                DateTime = DateTime.Now,
                Gig = gig,
                NotificationType = NotificationType.GigCanceled
            };

            var attendees = _dbContext.Attendances
                                      .Where(a => a.GigId == gigId)
                                      .Select(a => a.Attendee)
                                      .ToList();

            foreach (var attendee in attendees)
            {
                attendee.Notify(notification);
            }

            _dbContext.Gigs.Update(gig);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}