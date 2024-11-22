using API.Data;
using API.Models;
using API.Models.USV;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class FacultyService
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly ApiDbContext _dbContext;

        public FacultyService(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Faculty>> GetFacultiesAsync()
        {
            try
            {
                string url = "https://orar.usv.ro/orar/vizualizare/data/facultati.php?json";

                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                List<Faculty> faculties = JsonSerializer.Deserialize<List<Faculty>>(jsonResponse, options);

                return faculties;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare: {ex.Message}");
                return new List<Faculty>();
            }
        }
        public async Task<List<ProfessorUSV>> GetProffessorAsync()
        {
            try
            {
                string url = "https://orar.usv.ro/orar/vizualizare/data/cadre.php?json";

                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                List<ProfessorUSV> professor = JsonSerializer.Deserialize<List<ProfessorUSV>>(jsonResponse, options);

                return professor;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare: {ex.Message}");
                return new List<ProfessorUSV>();
            }
        }
        public async Task SyncProfessorsToDatabaseAsync()
        {
            try
            {
                var professors = await GetProffessorAsync();

                if (professors == null || professors.Count == 0)
                {
                    Console.WriteLine("Nu s-au găsit profesori în răspunsul API.");
                    return;
                }


                foreach (var professor in professors)
                {
                    var faculty = await _dbContext.Faculties
                        .FirstOrDefaultAsync(f => f.LongName == professor.FacultyName);

                    if (faculty == null)
                    {
                        Console.WriteLine($"Nu s-a găsit facultatea: {professor.FacultyName}");
                        continue;
                    }
                    var department = await _dbContext.Departments
                        .FirstOrDefaultAsync(d => d.Name == professor.FacultyName);

                    int? departmentId = department?.DepartmentID;
                    var user = new User
                    {
                        FacultyID = faculty.FacultyID,
                        FirstName = professor?.FirstName ?? "Unknown", 
                        LastName = professor?.LastName ?? "Unknown",  
                        Username = $"{(professor?.FirstName ?? "unknown").ToLower()}.{(professor?.LastName ?? "unknown").ToLower()}",
                        PasswordHash = "test",
                        Role = "Professor",
                        UniversityID = 1,
                        Status = "Active",
                        CreationDate = DateTime.UtcNow
                    };

                    var existingUser = await _dbContext.Users
                        .FirstOrDefaultAsync(u => u.FirstName == user.FirstName && u.LastName == user.LastName);

                    if (existingUser == null)
                    {
                        _dbContext.Users.Add(user);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        Console.WriteLine($"Utilizatorul {user.FirstName} {user.LastName} există deja.");
                        user = existingUser; 
                    }

                    var professorEntity = new Professor
                    {
                        UserID = user.UserID,
                        DepartmentID = null,
                        Title = "Lecturer", 
                        CreationDate = DateTime.UtcNow
                    };

                    var existingProfessor = await _dbContext.Professors
                        .FirstOrDefaultAsync(p => p.UserID == professorEntity.UserID);

                    if (existingProfessor == null)
                    {
                        _dbContext.Professors.Add(professorEntity);
                    }
                    else
                    {
                        Console.WriteLine($"Profesorul {user.FirstName} {user.LastName} există deja.");
                    }
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la sincronizarea profesorilor: {ex.Message}");
            }
        }
        public async Task<List<RommUSV>> GetRoomsAsync()
        {
            try
            {
                string url = "https://orar.usv.ro/orar/vizualizare/data/sali.php?json";

                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                List<RommUSV> rooms = JsonSerializer.Deserialize<List<RommUSV>>(jsonResponse, options);

                // Log pentru a verifica datele deserializate
                Console.WriteLine($"Rooms: {jsonResponse}");

                return rooms;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare: {ex.Message}");
                return new List<RommUSV>();
            }
        }
        public async Task SyncRoomsToDatabaseAsync()
        {
            var rooms = await GetRoomsAsync();

            if (rooms == null || rooms.Count == 0)
            {
                Console.WriteLine("Nu s-au găsit rooms în răspunsul API.");
                return;
            }
            foreach (var room in rooms)
            {
                var department = await _dbContext.Departments
                    .FirstOrDefaultAsync(d => d.Name == room.Name); 

                int? departmentId = department?.DepartmentID;

                var roomEntity = new Room
                {
                    DepartmentID = departmentId, 
                    Name = room?.ShortName ?? "Unknown", 
                    Location = room?.BuildingName ?? "Unknown", 
                    Capacity = null, 
                    Description = "No description",
                    CreationDate = DateTime.UtcNow
                };

                var existingRoom = await _dbContext.Rooms
                    .FirstOrDefaultAsync(r => r.Name == roomEntity.Name && r.Location == roomEntity.Location);

                if (existingRoom == null)
                {
                    _dbContext.Rooms.Add(roomEntity);  
                }
                else
                {
                    Console.WriteLine($"Sala {roomEntity.Name} deja există.");
                }
            }

            await _dbContext.SaveChangesAsync();
        }

    }
}
