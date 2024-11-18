namespace API.Models
{
    public class Secretary
    {
        public int SecretaryID { get; set; }
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual User User { get; set; }  // Foreign Key relationship
    }
}
