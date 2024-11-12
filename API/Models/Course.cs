namespace API.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public int CourseHolderID { get; set; }
        public int SpecializationID { get; set; }
        public int SubSpecializationID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreationDate { get; set; }

        public Professor CourseHolder { get; set; } // Relație cu Professor (titularul cursului)
        public Specialization Specialization { get; set; }
        public SubSpecialization SubSpecialization { get; set; }
    }

}
