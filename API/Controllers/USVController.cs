using API.Data;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class USVController : Controller
    {
        private readonly FacultyService _facultyService;
        private readonly ApiDbContext _context;
        public USVController(FacultyService facultyService, ApiDbContext context)
        {
            _facultyService = facultyService;
            _context = context;
        }

        [HttpGet("faculties")]
        public async Task<IActionResult> GetFaculties()
        {
            try
            {
                var faculties = await _facultyService.GetFacultiesAsync();

                if (faculties == null || !faculties.Any())
                {
                    return NotFound("No faculties found.");
                }
                foreach (var faculty in faculties)
                {
                    var existingFaculty = await _context.Faculties
                        .FirstOrDefaultAsync(f => f.FacultyID == faculty.FacultyID);

                    if (existingFaculty == null)
                    {
                        _context.Faculties.Add(faculty);
                    }
                }
                await _context.SaveChangesAsync();
                return Ok(faculties);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("sync-professors")]
        public async Task<IActionResult> SyncProfessors()
        {
            try
            {
                await _facultyService.SyncProfessorsToDatabaseAsync();

                return Ok("Professors have been synchronized successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("sync-rooms")]
        public async Task<IActionResult> SyncRooms()
        {
            try
            {
                await _facultyService.SyncRoomsToDatabaseAsync();

                return Ok("Rooms have been synchronized successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
