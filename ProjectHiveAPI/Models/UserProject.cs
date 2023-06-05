using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHiveAPI.Models
{
    [Table("user_project")]
    public class UserProject
    {
        public int Id { get; set; }
        [Column("date_added")]
        public DateTime? DateAdded { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("project_id")]
        public int ProjectId { get; set; }
    }
}
