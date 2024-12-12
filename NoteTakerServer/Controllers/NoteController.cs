using Microsoft.AspNetCore.Mvc;
using NoteTakerServer.Models;
using NoteTakerServer.Services;

namespace NoteTakerServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly NotesService _notesService;

        public NotesController(NotesService notesService)
        {
            _notesService = notesService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Note note)
        {
            _notesService.CreateNote(note);
            return Ok("Note created successfully.");
        }

        [HttpGet]
        public IActionResult Get()
        {
            var notes = _notesService.GetNotes();
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var note = _notesService.GetNoteById(id);
            if (note == null)
            {
                return NotFound("Note not found.");
            }
            return Ok(note);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Note note)
        {
            _notesService.UpdateNote(note);
            return Ok("Note updated successfully.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _notesService.DeleteNote(id);
            return Ok("Note deleted successfully.");
        }
    }
}