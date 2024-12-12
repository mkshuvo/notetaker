using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NoteTakerServer.Models
{
    public enum NoteType { Regular, Reminder, Todo, Bookmark }
    public class Note
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public NoteType Type { get; set; } = NoteType.Regular;
        public string? NoteId { get; set; }
        public string? NoteOwner { get; set; }
        public string? Text { get; set; }
        public DateTime? Reminder { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? IsComplete { get; set; } = false;
        public string? Url { get; set; }
    }
    public class NoteError
    {
        public string? Message { get; set; }
        public string? ErrorCode { get; set; }
    }
}
