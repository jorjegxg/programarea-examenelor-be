using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class ExamRequestRoom
    {
        [Key]
        public int Id { get; set; } 
        public int ExamRequestID { get; set; }
        public int RoomID { get; set; }

        public virtual ExamRequest ExamRequest { get; set; }
        public virtual Room Room { get; set; }
    }

}
