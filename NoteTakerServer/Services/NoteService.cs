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

        public void CreateNote(Note note)
        {
            _notes.Add(note);
            FileStorage.SaveNotes(_notes);
        }

        public List<Note> GetNotes()
        {
            return _notes;
        }

        public Note GetNoteById(int noteId)
        {
            return _notes.FirstOrDefault(n => n.NoteId == noteId) ?? new NoteError(){ IsError = true, StatusCode = "404", Message = "Note not found"};
        }

        public void UpdateNote(Note note)
        {
            var existingNote = _notes.FirstOrDefault(n => n.NoteId == note.NoteId);
            if (existingNote != null)
            {
                existingNote.Type = note.Type;
                existingNote.NoteOwner = note.NoteOwner;
                existingNote.Text = note.Text;
                existingNote.Reminder = note.Reminder;
                existingNote.DueDate = note.DueDate;
                existingNote.IsComplete = note.IsComplete;
                existingNote.URL = note.URL;
                FileStorage.SaveNotes(_notes);
            }
        }

        public void DeleteNote(int noteId)
        {
            var note = _notes.FirstOrDefault(n => n.NoteId == noteId);
            if (note != null)
            {
                _notes.Remove(note);
                FileStorage.SaveNotes(_notes);
            }
        }
    }
}