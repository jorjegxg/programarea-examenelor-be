using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Professor
    {
        [Key]
        public int ProfID { get; set; }
        public int UserID { get; set; }
        public int? DepartmentID { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual Department? Department { get; set; }
        public virtual User User { get; set; } // Relația cu User

    }
}
