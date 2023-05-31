namespace ProjectHiveAPI.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public int Priority { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public int ManagerId { get; set; }
    }
}
