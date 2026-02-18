namespace StudentNotesApi.Models
{
    public class Subject
    {
        public int Id { get; set; }                // Primary Key
        public string Name { get; set; } = "";     // Subject name

        // Foreign key to Student
        public int StudentId { get; set; }
        public Student? Student { get; set; }

        // One subject can have many notes
        public List<Note> Notes { get; set; } = new();
    }
}
