namespace ProjectHiveAPI.Models
{
    public class SubTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
        public int TaskId { get; set; }
    }
}
