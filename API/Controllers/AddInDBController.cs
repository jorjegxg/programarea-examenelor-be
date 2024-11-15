using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AddInDBController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public AddInDBController(ApiDbContext context)
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
        [HttpPost("course")]
        public async Task<IActionResult> CreateCourse([FromBody] Course course)
        {
            if (course == null)
            {
                return BadRequest("Invalid course data.");
            }

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.CourseID }, course);
        }

        // POST pentru Department
        [HttpPost("department")]
        public async Task<IActionResult> CreateDepartment([FromBody] Department department)
        {
            if (department == null)
            {
                return BadRequest("Invalid department data.");
            }

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartment", new { id = department.DepartmentID }, department);
        }

        // POST pentru Faculty
        [HttpPost("faculty")]
        public async Task<IActionResult> CreateFaculty([FromBody] Faculty faculty)
        {
            if (faculty == null)
            {
                return BadRequest("Invalid faculty data.");
            }

            _context.Faculties.Add(faculty);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFaculty", new { id = faculty.FacultyID }, faculty);
        }

        // POST pentru Group
        [HttpPost("group")]
        public async Task<IActionResult> CreateGroup([FromBody] Group group)
        {
            if (group == null)
            {
                return BadRequest("Invalid group data.");
            }

            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroup", new { id = group.GroupID }, group);
        }

        // POST pentru LabHolders
        [HttpPost("labholder")]
        public async Task<IActionResult> CreateLabHolder([FromBody] LabHolders labHolders)
        {
            if (labHolders == null)
            {
                return BadRequest("Invalid lab holder data.");
            }

            _context.LabHolders.Add(labHolders);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLabHolder", new { id = labHolders.LabId }, labHolders);
        }

        // POST pentru Professor
        [HttpPost("professor")]
        public async Task<IActionResult> CreateProfessor([FromBody] Professor professor)
        {
            if (professor == null)
            {
                return BadRequest("Invalid professor data.");
            }

            _context.Professors.Add(professor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfessor", new { id = professor.ProfID }, professor);
        }

        [HttpPost("specialization")]
        public async Task<IActionResult> CreateSpecialization([FromBody] Specialization specialization)
        {
            if (specialization == null)
            {
                return BadRequest("Invalid specialization data.");
            }

            // Verificăm dacă Facultatea există
            var faculty = await _context.Faculties.FindAsync(specialization.FacultyID);
            if (faculty == null)
            {
                return NotFound("Faculty not found.");
            }

            // Asociem facultatea cu specializarea
            specialization.Faculty = faculty;

            // Salvăm specializarea în baza de date
            _context.Specializations.Add(specialization);
            await _context.SaveChangesAsync();

            // Returnăm specializarea creată
            return Ok(specialization);
        }


        // POST pentru Student
        [HttpPost("student")]
        public async Task<IActionResult> CreateStudent([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest("Invalid student data.");
            }

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.StudentID }, student);
        }
        [HttpPost]
        public async Task<IActionResult> CreateExamRequest([FromBody] ExamRequest examRequest)
        {
            if (examRequest == null)
            {
                return BadRequest("Invalid request data.");
            }

            // Validare: Verifică dacă toate entitățile asociate există
            var group = await _context.Groups.FindAsync(examRequest.GroupID);
            if (group == null)
            {
                return NotFound("Group not found.");
            }

            var course = await _context.Courses.FindAsync(examRequest.CourseID);
            if (course == null)
            {
                return NotFound("Course not found.");
            }

            var room = await _context.Rooms.FindAsync(examRequest.RoomID);
            if (room == null)
            {
                return NotFound("Room not found.");
            }

            var labHolder = await _context.LabHolders
                .Include(lh => lh.Course)
                .Include(lh => lh.Professor)
                .Where(lh => lh.LabId == examRequest.AssistantID && lh.CourseID == examRequest.CourseID)
                .FirstOrDefaultAsync();

            if (labHolder == null)
            {
                return NotFound("Assistant not found in the specified course.");
            }

            var assistant = labHolder.Professor;
            if (assistant == null)
            {
                return NotFound("Assistant not found.");
            }

            var session = await _context.Sessions.FindAsync(examRequest.SessionID);
            if (session == null)
            {
                return NotFound("Session not found.");
            }

            // Salvarea cererii în baza de date
            examRequest.CreationDate = DateTime.UtcNow;  // Setează data de creare
            examRequest.Assistant = assistant;
            examRequest.Course = course;
            examRequest.Room = room;
            examRequest.Session = session;
            _context.ExamRequests.Add(examRequest);
            await _context.SaveChangesAsync();

            // Returnează răspunsul cu locația cererii create
            return Ok(examRequest);
        }

    }
}
