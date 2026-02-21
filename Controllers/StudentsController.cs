using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentNotesApi.Data;
using StudentNotesApi.Dtos;
using StudentNotesApi.Models;

namespace StudentNotesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public StudentsController(AppDbContext db) => _db = db;

        private static StudentDto ToDto(Student s) => new StudentDto
        {
            Id = s.Id,
            Name = s.Name,
            Email = s.Email
        };

        // GET: api/students
        [HttpGet]
        public async Task<ActionResult<List<StudentDto>>> GetAll()
        {
            var students = await _db.Students.AsNoTracking().ToListAsync();
            return students.Select(ToDto).ToList();
        }

        // GET: api/students/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<StudentDto>> GetById(int id)
        {
            var student = await _db.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            if (student == null) return NotFound();
            return ToDto(student);
        }

        // POST: api/students
        [HttpPost]
        public async Task<ActionResult<StudentDto>> Create(CreateStudentDto dto)
        {
            var student = new Student
            {
                Name = dto.Name,
                Email = dto.Email
            };

            _db.Students.Add(student);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = student.Id }, ToDto(student));
        }

        // PUT: api/students/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateStudentDto dto)
        {
            var student = await _db.Students.FirstOrDefaultAsync(s => s.Id == id);
            if (student == null) return NotFound();

            student.Name = dto.Name;
            student.Email = dto.Email;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/students/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _db.Students.FirstOrDefaultAsync(s => s.Id == id);
            if (student == null) return NotFound();

            _db.Students.Remove(student);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
