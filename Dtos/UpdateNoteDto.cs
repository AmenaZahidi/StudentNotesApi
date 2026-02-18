namespace StudentNotesApi.Dtos
{
    public class UpdateNoteDto
    {
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public bool IsImportant { get; set; }
    }
}
