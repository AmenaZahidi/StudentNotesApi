using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentNotesApi.Data;
using StudentNotesApi.Dtos;
using StudentNotesApi.Models;

namespace StudentNotesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SubjectsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public SubjectsController(AppDbContext db) => _db = db;

        private static SubjectDto ToDto(Subject s) => new SubjectDto
        {
            Id = s.Id,
            Name = s.Name
        };

        // GET: api/subjects
        [HttpGet]
        public async Task<ActionResult<List<SubjectDto>>> GetAll()
        {
            var subjects = await _db.Subjects.AsNoTracking().ToListAsync();
            return subjects.Select(ToDto).ToList();
        }

        // GET: api/subjects/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SubjectDto>> GetById(int id)
        {
            var subject = await _db.Subjects.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            if (subject == null) return NotFound();
            return ToDto(subject);
        }

        // POST: api/subjects
        [HttpPost]
        public async Task<ActionResult<SubjectDto>> Create(CreateSubjectDto dto)
        {
            var studentExists = await _db.Students.AnyAsync(x => x.Id == dto.StudentId);
            if (!studentExists) return BadRequest("StudentId does not exist.");

            var subject = new Subject { Name = dto.Name, StudentId = dto.StudentId };

            _db.Subjects.Add(subject);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = subject.Id }, ToDto(subject));
        }

        // PUT: api/subjects/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateSubjectDto dto)
        {
            var subject = await _db.Subjects.FirstOrDefaultAsync(s => s.Id == id);
            if (subject == null) return NotFound();

            subject.Name = dto.Name;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // PATCH: api/subjects/5
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, PatchSubjectDto dto)
        {
            var subject = await _db.Subjects.FirstOrDefaultAsync(s => s.Id == id);
            if (subject == null) return NotFound();

            if (!string.IsNullOrWhiteSpace(dto.Name))
                subject.Name = dto.Name;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/subjects/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var subject = await _db.Subjects.FirstOrDefaultAsync(s => s.Id == id);
            if (subject == null) return NotFound();

            _db.Subjects.Remove(subject);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
