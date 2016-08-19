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
        [StringLength(100)]
        public string Name { get; set; }

        public byte[] Content { get; set; }

        [StringLength(140)]
        public string Description { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime Date { get; set; }

        public virtual User User { get; set; }
    }
}
