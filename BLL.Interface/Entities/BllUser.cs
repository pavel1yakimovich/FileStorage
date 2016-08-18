namespace BLL.Interface.Entities
{
    public class BllUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string About { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public BllRole Role { get; set; }
    }
}