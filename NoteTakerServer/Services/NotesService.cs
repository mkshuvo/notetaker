using NoteTakerServer.DataStorage;
using NoteTakerServer.Models;

namespace NoteTakerServer.Services
{
    public class NotesService
    {
        private readonly List<Note> _notes;

        public NotesService()
        {
            _notes = FileStorage.LoadNotes();
        }

        public string CreateNote(Note note, string userId)
        {
            note.NoteId = Guid.NewGuid().ToString();
            // use the note owner to identify the user
            note.NoteOwner = userId;
            _notes.Add(note);
            FileStorage.SaveNotes(_notes);
            return note.NoteId;
        }

        public List<Note> GetNotes()
        {
            return _notes;
        }

        public Note GetNoteById(string noteId)
        {
            return _notes.FirstOrDefault(n => n.NoteId == noteId);
        }

        public Note UpdateNote(Note note)
        {
            if (note == null)
            {
                return null;
            }
            var existingNote = GetNoteById(note.NoteId);
            if (existingNote != null)
            {
                existingNote.Type = note.Type;
                existingNote.Text = note.Text;
                existingNote.Reminder = note.Reminder;
                existingNote.DueDate = note.DueDate;
                existingNote.IsComplete = note.IsComplete;
                existingNote.Url = note.Url;
                FileStorage.SaveNotes(_notes);
            }
            return existingNote;
        }

        public Note DeleteNote(string noteId)
        {
            var note = GetNoteById(noteId);
            if (note != null)
            {
                _notes.Remove(note);
                FileStorage.SaveNotes(_notes);
            }
            return note;
        }
        public Note MarkNoteCompleted(string noteId)
        {
            var note = GetNoteById(noteId);
            if (note != null)
            {
                note.IsComplete = true;
                FileStorage.SaveNotes(_notes);
            }
            return note;
        }
        public List<Note> AllNotes(string userId)
        {
            return _notes.Where(n => n.NoteOwner == userId).ToList();
        }
        public List<Note> TodaysDueNotes(string userId)
        {
            return _notes.Where(n => n.IsComplete == false && n.NoteOwner == userId && n.DueDate == DateTime.Today).ToList();
        }
        public List<Note> WeeksDueNotes(string userId)
        {
            return _notes.Where(n => n.IsComplete == false && n.NoteOwner == userId && n.DueDate == DateTime.Today.AddDays(7)).ToList();
        }
        public List<Note> MonthsDueNotes(string userId)
        {
            return _notes.Where(n => n.IsComplete == false && n.NoteOwner == userId && n.DueDate == DateTime.Today.AddDays(30)).ToList();
        }
    }
}