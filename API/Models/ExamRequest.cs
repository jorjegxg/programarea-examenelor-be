using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class ExamRequest
    {
        [Key]
        public int RequestID { get; set; }
        public int GroupID { get; set; }
        public int CourseID { get; set; }
        public int AssistantID { get; set; }
        public int SessionID { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan TimeStart { get; set; }
        public TimeSpan Duration { get; set; }
        public string Details { get; set; }
        public string Status { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual Group Group { get; set; }
        public virtual Course Course { get; set; }
        public virtual Professor Assistant { get; set; }
        public virtual Session Session { get; set; }

        [JsonIgnore] 
        public List<ExamRequestRoom> ExamRequestRooms { get; set; }
    }


}
