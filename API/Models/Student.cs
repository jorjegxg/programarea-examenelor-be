using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public int UserID { get; set; }
        public int GroupID { get; set; }
        public DateTime CreationDate { get; set; }

        public User User { get; set; }  // Foreign Key relationship
        public Group Group { get; set; }  // Foreign Key relationship
    }
}
