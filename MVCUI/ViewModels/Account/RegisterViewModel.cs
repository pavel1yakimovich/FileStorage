using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCUI.ViewModels.Account
{
    public class RegisterViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Enter your name")]
        [Required(ErrorMessage = "The field can not be empty!")]
        //[RegularExpression(@"[A-Za-z0-9._%+-]", ErrorMessage = "Wrong name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [StringLength(50, ErrorMessage = "The password must contain at least {2} characters", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Enter your password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm the password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm the password")]
        [Compare("Password", ErrorMessage = "Passwords must match")]
        public string ConfirmPassword { get; set; }

        [ScaffoldColumn(false)]
        public RoleViewModel Role { get; set; }
    }
}

