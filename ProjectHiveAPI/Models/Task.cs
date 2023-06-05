using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHiveAPI.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Column("start_date")]
        public DateTime StartDate { get; set; }
        [Column("end_date")]
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public byte Priority { get; set; }
        [Column("project_id")]
        public int ProjectId { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("manager_id")]
        public int ManagerId { get; set; }
    }
}
