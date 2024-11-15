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

        [HttpPost("add")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User data is null.");
            }

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
            if (existingUser != null)
            {
                return Conflict("A user with the same username already exists.");
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok("User added successfully.");
        }
        [HttpGet("students")]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var students = await _context.Students
                    .Include(s => s.User) // Include relația cu tabela `Users`
                    .Include(s => s.Group) // Include relația cu tabela `Groups`
                    .ToListAsync();

                if (students == null || !students.Any())
                {
                    return NotFound("No students found.");
                }

                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
