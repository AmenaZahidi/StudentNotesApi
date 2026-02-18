namespace StudentNotesApi.Dtos
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public bool IsImportant { get; set; }
        public DateTime CreatedAt { get; set; }
        public int SubjectId { get; set; }
    }
}
