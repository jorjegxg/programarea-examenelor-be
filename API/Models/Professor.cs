using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Professor
    {
        [Key]
        public int ProfID { get; set; }
        public int UserID { get; set; }
        public int DepartmentID { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }

        public User User { get; set; } // Relație cu User
        public Department Department { get; set; } // Relație cu Department
    }
}
