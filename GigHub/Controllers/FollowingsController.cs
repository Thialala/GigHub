using System.Linq;
using GigHub.Data;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace GigHub.Controllers
{
    [Route("api/followings")]
    [ApiController]
    [Authorize]
    public class FollowingsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public FollowingsController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [HttpPost]
        public IActionResult Follow([FromBody] FollowingDto dto)
        {
            var followerId = _userManager.GetUserId(User);
            var exists = _dbContext.Followings.Any(f => f.FollowerId == followerId && f.FolloweeId == dto.FolloweeId);
            if (exists)
            {
                return BadRequest("The attendance already exists.");
            }

            var following = new Following { FolloweeId = dto.FolloweeId, FollowerId = followerId};
            _dbContext.Followings.Add(following);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}