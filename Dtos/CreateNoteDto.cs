namespace StudentNotesApi.Dtos
{
    public class CreateNoteDto
    {
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public bool IsImportant { get; set; }
        public int SubjectId { get; set; }
    }
}
