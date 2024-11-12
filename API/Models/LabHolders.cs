using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class LabHolders
    {
        [Key]
        public int LabId { get; set; }
        public int ProfID { get; set; }
        public int CourseID { get; set; }
        public DateTime CreationDate { get; set; }

        public Professor Professor { get; set; } // Relație cu Professor
        public Course Course { get; set; } // Relație cu Course
    }

}
