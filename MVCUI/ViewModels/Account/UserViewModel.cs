using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCUI.ViewModels.Account
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        public string Email { get; set; }
        [Display(Name = "About")]
        public string About { get; set; }
        public RoleViewModel Role { get; set; }
    }
}