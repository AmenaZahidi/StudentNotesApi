using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentNotesApi.Data;
using StudentNotesApi.Dtos;
using StudentNotesApi.Models;

namespace StudentNotesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly AppDbContext _db;
        public NotesController(AppDbContext db) => _db = db;

        private static NoteDto ToDto(Note n) => new NoteDto
        {
            Id = n.Id,
            Title = n.Title,
            Content = n.Content,
            IsImportant = n.IsImportant,
            CreatedAt = n.CreatedAt,
            SubjectId = n.SubjectId
        };

        // GET: api/notes?subjectId=1&important=true
        [HttpGet]
        public async Task<ActionResult<List<NoteDto>>> GetAll([FromQuery] int? subjectId, [FromQuery] bool? important)
        {
            var query = _db.Notes.AsNoTracking().AsQueryable();

            if (subjectId.HasValue)
                query = query.Where(n => n.SubjectId == subjectId.Value);

            if (important.HasValue)
                query = query.Where(n => n.IsImportant == important.Value);

            var notes = await query.ToListAsync();
            return notes.Select(ToDto).ToList();
        }

        // GET: api /notes/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<NoteDto>> GetById(int id)
        {
            var note = await _db.Notes.AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
            if (note == null) return NotFound();
            return ToDto(note);
        }

        // POST: api/notes
        [HttpPost]
        public async Task<ActionResult<NoteDto>> Create(CreateNoteDto dto)
        {
            var subjectExists = await _db.Subjects.AnyAsync(s => s.Id == dto.SubjectId);
            if (!subjectExists) return BadRequest("SubjectId does not exist.");

            var note = new Note
            {
                Title = dto.Title,
                Content = dto.Content,
                IsImportant = dto.IsImportant,
                SubjectId = dto.SubjectId
            };

            _db.Notes.Add(note);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = note.Id }, ToDto(note));
        }

        // PUT: api/notes/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateNoteDto dto)
        {
            var note = await _db.Notes.FirstOrDefaultAsync(n => n.Id == id);
            if (note == null) return NotFound();

            note.Title = dto.Title;
            note.Content = dto.Content;
            note.IsImportant = dto.IsImportant;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        // PATCH: api/notes/5
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, PatchNoteDto dto)
        {
            var note = await _db.Notes.FirstOrDefaultAsync(n => n.Id == id);
            if (note == null) return NotFound();

            if (!string.IsNullOrWhiteSpace(dto.Title)) note.Title = dto.Title;
            if (!string.IsNullOrWhiteSpace(dto.Content)) note.Content = dto.Content;
            if (dto.IsImportant.HasValue) note.IsImportant = dto.IsImportant.Value;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/notes/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var note = await _db.Notes.FirstOrDefaultAsync(n => n.Id == id);
            if (note == null) return NotFound();

            _db.Notes.Remove(note);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
