using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.Data;
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

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok("User added successfully.");
        }
    }
}
