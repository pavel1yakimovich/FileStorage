namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class File
    {
        public int Id { get; set; }

        public int User_Id { get; set; }

        public bool IsPublic { get; set; }

        [Required]
        public string Name { get; set; }

        public byte[] Content { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime Date { get; set; }

        [Required]
        public string Type { get; set; }

        public virtual User User { get; set; }
    }
}