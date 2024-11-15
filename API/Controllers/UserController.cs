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
                    .Include(e => e.Group)         // Include relația cu tabela `Groups`
                    .Include(e => e.Course)        // Include relația cu tabela `Courses`
                    .Include(e => e.Room)          // Include relația cu tabela `Rooms`
                    .Include(e => e.Assistant)     // Include relația cu tabela `Professors` (Assistant)
                    .Include(e => e.Session)       // Include relația cu tabela `Sessions`
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
