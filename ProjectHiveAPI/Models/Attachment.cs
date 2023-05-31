namespace ProjectHiveAPI.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public int TaskId { get; set; }
    }
}
