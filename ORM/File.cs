using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ORM
{
    public class File
    {
        public int Id { get; set; }

        public int User_Id { get; set; }

        public bool IsPublic { get; set; }

        [Required]
        public string Name { get; set; }

        public byte[] Content { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string Type { get; set; }

        public virtual User User { get; set; }
    }
}
