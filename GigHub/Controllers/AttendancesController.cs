using System.Linq;
using GigHub.Data;
using GigHub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace GigHub.Controllers
{
    [Route("api/attendances")]
    [ApiController]
   // [Authorize]
    public class AttendancesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public AttendancesController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [HttpPost]
        public IActionResult Attend([FromBody] AttendanceDto dto)
        {
            var userId = _userManager.GetUserId(User);
            var exists = _dbContext.Attendances.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId);
            if (exists)
            {
                return BadRequest("The attendance already exists.");
            }

            var attendance = new Attendance { AttendeeId = userId, GigId = dto.GigId };
            _dbContext.Add(attendance);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}