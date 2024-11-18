namespace API.Models
{
    public class User
    {
        public int UserID { get; set; }
        public int FacultyID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public int UniversityID { get; set; }
        public string Status { get; set; }
        public DateTime CreationDate { get; set; }


    }
}
