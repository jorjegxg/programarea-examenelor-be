using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class RoomRequested
    {
        [Key]
        public int RequestID { get; set; }
        public int RoomID { get; set; }
        public DateTime CreationDate { get; set; }

        public ExamRequest ExamRequest { get; set; } // Relație cu ExamRequest
        public Room Room { get; set; } // Relație cu Room
    }

}
