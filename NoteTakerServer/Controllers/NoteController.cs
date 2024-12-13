using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteTakerServer.Models;
using NoteTakerServer.Services;
using System.Security.Claims;
namespace NoteTakerServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly NotesService _notesService;
        private readonly AuthService _authService;
        public NotesController(NotesService notesService, AuthService authService)
        {
            _notesService = notesService;
            _authService = authService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Note note)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized("User email not found in token");
            }

            var user = _authService.GetUserById(userEmail);
            if (user == null)
            {
                return Unauthorized("User not found");
            }

            var noteId = _notesService.CreateNote(note, user.UserId);
            var response = new { Id = noteId };
            return Ok(response);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var notes = _notesService.GetNotes();
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var note = _notesService.GetNoteById(id);
            if (note == null)
            {
                var errorObj = new NoteError() { ErrorCode = "404", Message = "Note not found." };
                return NotFound(errorObj);
            }
            return Ok(note);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Note note)
        {
            var updatedNote = _notesService.UpdateNote(note);
            if(updatedNote == null)
            {
                var errorObj = new NoteError() { ErrorCode = "400", Message = "Failed to update note." };
                return BadRequest(errorObj);
            }
            var messageObj = new { Message = "Note updated successfully." };
            return Ok(messageObj);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var note = _notesService.DeleteNote(id);
            if (note == null)
            {
                var errorObj = new NoteError() { ErrorCode = "404", Message = "Note not found." };
                return NotFound(errorObj);
            }
            return Ok("Note deleted successfully.");
        }
        [HttpGet("user/{id}/due/{dueType}")]
        public IActionResult GetDue(string id, string dueType)
        {
            List<Note> notes = dueType.ToLower() switch
            {
                "today" => _notesService.TodaysDueNotes(id),
                "week" => _notesService.WeeksDueNotes(id),
                "month" => _notesService.MonthsDueNotes(id),
                _ => null
            };

            if (notes == null)
            {
                var errorObj = new NoteError() { ErrorCode = "404", Message = "Note not found." };
                return NotFound(errorObj);
            }

            return Ok(notes);
        }
        [HttpGet("user/{id}")]
        public IActionResult UserNotes(string id)
        {
            var notes = _notesService.AllNotes(id);
            return Ok(notes);
        }
        [HttpPatch("{id}/complete")]
        public IActionResult MarkCompleted(string id)
        {
            var note = _notesService.MarkNoteCompleted(id);
            if (note == null)
            {
                var errorObj = new NoteError() { ErrorCode = "404", Message = "Note not found." };
                return NotFound(errorObj);
            }
            return Ok("Note marked as completed.");
        }
    }
}