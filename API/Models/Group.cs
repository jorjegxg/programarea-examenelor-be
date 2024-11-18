namespace API.Models
{
    public class Group
    {
        public int GroupID { get; set; }
        public int SpecializationID { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual Specialization Specialization { get; set; } // Relație cu Specialization
    }
}
