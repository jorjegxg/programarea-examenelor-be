namespace API.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public int FacultyID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual Faculty Faculty { get; set; }  // Foreign Key relationship

    }
}
