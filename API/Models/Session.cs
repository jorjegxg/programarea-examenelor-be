namespace API.Models
{
    public class Session
    {
        public int SessionID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
    }

}
