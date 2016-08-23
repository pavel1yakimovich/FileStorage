using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MVCUI.ViewModels.File;

namespace MVCUI.ViewModels.Account
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        public RoleViewModel Role { get; set; }
        public IEnumerable<FileViewModel> Files { get; set; }
    }
}