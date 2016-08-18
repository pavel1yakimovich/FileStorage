using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCUI.ViewModels
{
    public enum Role
    {
        Administrator = 1,
        User
    }

    public class UserViewModel
    {
        public int Id { get; set; }
        [Display(Name = "User name")]
        public string UserName { get; set; }
        public String Role { get; set; }
    }
}