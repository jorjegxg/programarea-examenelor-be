namespace API.Models
{
    public class SubSpecialization
    {
        public int SubSpecializationID { get; set; }
        public int SpecializationID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

        public Specialization Specialization { get; set; } // Relație cu Specialization
    }

}
