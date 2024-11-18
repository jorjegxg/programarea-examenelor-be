namespace API.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public int ProfID { get; set; }
        public int SpecializationID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual Professor Professor { get; set; } // Relație cu Professor (titularul cursului)
        public virtual Specialization Specialization { get; set; }
    }

}
