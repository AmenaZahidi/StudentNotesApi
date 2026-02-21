using Microsoft.EntityFrameworkCore;
using StudentNotesApi.Models;

namespace StudentNotesApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students => Set<Student>();
        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<Note> Notes => Set<Note>();
        public DbSet<AppUser> Users => Set<AppUser>();
    }
}
