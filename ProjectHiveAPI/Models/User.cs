using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHiveAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Login { get; set; }
        public string? Email { get; set; }

        [Column("password_hash")]
        public string? Password { get; set; }

        [Column("first_name")]
        public string? FirstName { get; set; }

        [Column("last_last")]
        public string? LastName { get; set; }
        public string? Role { get; set; }

        [Column("profile_image")]
        public string? ProfileImage { get; set; }
    }
}
