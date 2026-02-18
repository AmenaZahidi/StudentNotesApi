namespace StudentNotesApi.Models
{
    public class Student
    {
        public int Id { get; set; }              // Primary Key
        public string Name { get; set; } = "";   // Student name
        public string Email { get; set; } = "";  // Student email

        // One student can have many subjects
        public List<Subject> Subjects { get; set; } = new();
    }
}
