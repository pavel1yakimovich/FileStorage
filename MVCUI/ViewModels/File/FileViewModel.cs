using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MVCUI.ViewModels.User;

namespace MVCUI.ViewModels.File
{
    public class FileViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Public file")]
        public bool IsPublic { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public string Description { get; set; }
        [Display(Name = "Date of last change")]
        public DateTime Date { get; set; }
        [Display(Name = "Owner")]
        public UserViewModel User { get; set; }
    }
}