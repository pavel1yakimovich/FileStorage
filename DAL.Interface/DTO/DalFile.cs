using System;

namespace DAL.Interface.DTO
{
    public class DalFile : IEntity
    {
        public int Id { get; set; }
        public bool IsPublic { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public byte[] Content { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public DalUser User { get; set; }
    }
}
