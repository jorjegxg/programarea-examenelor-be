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

            var faculty = await _context.Faculties.FindAsync(specialization.FacultyID);
            if (faculty == null)
            {
                return NotFound("Faculty not found.");
            }

            specialization.Faculty = faculty;

            _context.Specializations.Add(specialization);
            await _context.SaveChangesAsync();

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
        [HttpPost("CreateExamRequest")]
        public async Task<IActionResult> CreateExamRequest([FromBody] CreateExamRequestDto examRequestDto)
        {
            if (examRequestDto == null)
            {
                return BadRequest("Invalid request data.");
            }

            var group = await _context.Groups.FindAsync(examRequestDto.GroupID);
            if (group == null)
            {
                return NotFound("Group not found.");
            }

            var course = await _context.Courses.FindAsync(examRequestDto.CourseID);
            if (course == null)
            {
                return NotFound("Course not found.");
            }

            var rooms = await _context.Rooms
                .Where(r => examRequestDto.RoomIDs.Contains(r.RoomID))
                .ToListAsync();
            if (rooms.Count != examRequestDto.RoomIDs.Count)
            {
                return NotFound("One or more rooms not found.");
            }

            var labHolder = await _context.LabHolders
                .Include(lh => lh.Course)
                .Include(lh => lh.Professor)
                .Where(lh => lh.LabId == examRequestDto.AssistantID && lh.CourseID == examRequestDto.CourseID)
                .FirstOrDefaultAsync();

            if (labHolder == null)
            {
                return NotFound("Assistant not found in the specified course.");
            }

            var session = await _context.Sessions.FindAsync(examRequestDto.SessionID);
            if (session == null)
            {
                return NotFound("Session not found.");
            }

            // Creare obiect `ExamRequest`
            var examRequest = new ExamRequest
            {
                GroupID = examRequestDto.GroupID,
                CourseID = examRequestDto.CourseID,
                AssistantID = examRequestDto.AssistantID,
                SessionID = examRequestDto.SessionID,
                Type = examRequestDto.Type,
                Date = examRequestDto.Date,
                TimeStart = examRequestDto.TimeStart,
                Duration = examRequestDto.Duration,
                Details = examRequestDto.Details,
                Status = examRequestDto.Status,
                CreationDate = DateTime.UtcNow,
                ExamRequestRooms = rooms.Select(r => new ExamRequestRoom
                {
                    RoomID = r.RoomID
                }).ToList()
            };
            examRequest.Session = session;
            examRequest.Course = course;
            examRequest.Group = group;
            examRequest.Assistant = labHolder.Professor;

            _context.ExamRequests.Add(examRequest);
            await _context.SaveChangesAsync();

            return Ok(examRequest);
        }


    }
}
