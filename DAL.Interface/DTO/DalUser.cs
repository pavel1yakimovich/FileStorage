using System.Collections.Generic;

namespace DAL.Interface.DTO
{
    public class DalUser : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string About { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IEnumerable<DalRole> Roles { get; set; }
    }
}