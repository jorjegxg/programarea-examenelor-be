namespace API.Models
{
    public class Modifications
    {
        public int ModificationID { get; set; }
        public int UserID { get; set; }
        public string Table { get; set; }
        public string Column { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual User User { get; set; }
    }

}
