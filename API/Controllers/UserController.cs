using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using Microsoft.EntityFrameworkCore;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public UserController(ApiDbContext context)
        {
            _context = context;
        }
        [HttpGet("examrequests")]
        public async Task<IActionResult> GetAllExamRequests()
        {
            try
            {
                var examRequests = await _context.ExamRequests
                .Include(e => e.Group)
                    .ThenInclude(g => g.Specialization)
                    .ThenInclude(s => s.Faculty)
                .Include(e => e.Course)
                    .ThenInclude(c => c.Professor)
                        .ThenInclude(p => p.Department) 
                .Include(e => e.Course)
                    .ThenInclude(c => c.Professor)
                        .ThenInclude(p => p.User) 
                .Include(e => e.Assistant)
                    .ThenInclude(a => a.User) 
                .Include(e => e.Assistant)
                    .ThenInclude(a => a.Department) 
                .Include(e => e.Session)
                .Include(e => e.ExamRequestRooms)
                    .ThenInclude(er => er.Room)
                .ToListAsync();



                if (examRequests == null || !examRequests.Any())
                {
                    return NotFound("No exam requests found.");
                }

                return Ok(examRequests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
