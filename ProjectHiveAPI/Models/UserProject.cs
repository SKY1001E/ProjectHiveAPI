namespace ProjectHiveAPI.Models
{
    public class UserProject
    {
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
    }
}
