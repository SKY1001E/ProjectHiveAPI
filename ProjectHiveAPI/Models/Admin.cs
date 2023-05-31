namespace ProjectHiveAPI.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }
        public int SubscriptionPlanID { get; set; }
    }
}
