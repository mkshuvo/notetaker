namespace NoteTakerServer.Models
{
    public class Note
    {
        public enum NoteType { Regular, Reminder, Todo, Bookmark }
        public NoteType Type { get; set; }
        public int NoteId { get; set; }
        public int NoteOwner { get; set; }
        public string? Text { get; set; }
        public DateTime? Reminder { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? IsComplete { get; set; }
        public string? URL { get; set; }
    }
    public class NoteError : Note
    {
        public bool IsError { get; set; }
        public string? Message { get; set; }
        public string? StatusCode { get; set; }
    }
}
