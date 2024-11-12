using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class ExamRequest
    {
        [Key]
        public int RequestID { get; set; }
        public int GroupID { get; set; }
        public int CourseID { get; set; }
        public int RoomID { get; set; }
        public int AssistantID { get; set; }
        public int SessionID { get; set; }
        public string Type { get; set; } // ex: exam, colloq
        public DateTime Date { get; set; }
        public TimeSpan TimeStart { get; set; }
        public TimeSpan Duration { get; set; }
        public string Details { get; set; }
        public string Status { get; set; }
        public DateTime CreationDate { get; set; }

        public Group Group { get; set; }
        public Course Course { get; set; }
        public Room Room { get; set; }
        public Professor Assistant { get; set; }
        public Session Session { get; set; }
    }

}
