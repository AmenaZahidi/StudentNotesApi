namespace StudentNotesApi.Models
{
    public class Note
    {
        public int Id { get; set; }                   // Primary Key
        public string Title { get; set; } = "";       // Note title
        public string Content { get; set; } = "";     // Note content
        public bool IsImportant { get; set; }         // Important flag
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign key to Subject
        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }
    }
}
