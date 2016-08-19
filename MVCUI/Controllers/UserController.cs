using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Services;
using MVCUI.Infrastructure.Mappers;
using MVCUI.ViewModels;
using MVCUI.ViewModels.User;

namespace MVCUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IRoleService roleService;
        private readonly IUserService userService;

        public UserController(IUserService userService, IRoleService roleService)
        {
            this.userService = userService;
            this.roleService = roleService;
        }

        [ActionName("Index")]
        public ActionResult GetAllUsers()
        {
            return View(userService.GetAllUserEntities().Select(user => user.ToMvcUser()));
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisterViewModel registerViewModel)
        {
            registerViewModel.Role = roleService.GetRoleEntity(1).ToMvcRole();

            userService.CreateUser(registerViewModel.ToBllUser());
            return RedirectToAction("Index");
        }
    }
}