using System.Xml.Linq;

namespace ProjectHiveAPI.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime CalendarDate { get; set; }
        public string? ProjectId { get; set; }
        public string? UserId { get; set; }
    }
}
