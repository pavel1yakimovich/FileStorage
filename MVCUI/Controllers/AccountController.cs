﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interface.Services;
using MVCUI.Infrastructure.Mappers;
using MVCUI.Providers;
using MVCUI.ViewModels;
using MVCUI.ViewModels.Account;

namespace MVCUI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly IFileService fileService;

        public AccountController(IUserService userService, IFileService fileService)
        {
            this.fileService = fileService;
            this.userService = userService;
        }

        [Authorize(Roles = "Admin")]
        [ActionName("Index")]
        public ActionResult GetAllUsers()
        {
            var list = userService.GetAllUserEntities().Select(user => user.ToMvcUser());

            foreach (UserViewModel item in list)
            {
                item.Files = fileService.GetAllFileEntitiesOfUser(item.Name).Select(file => file.ToMvcFile());
            }
            return View(list);
        }

        [AllowAnonymous]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogIn(LogOnViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.Name, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Name, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("All", "File");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Wrong password or name");
                }
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("LogIn", "Account");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var anyUser = userService.GetAllUserEntities().Any(u => u.Name.Contains(registerViewModel.Name));

                if (anyUser)
                {
                    ModelState.AddModelError("", "User with this name is already registered");
                    return View(registerViewModel);
                }

                MembershipUser membershipUser =
                    ((CustomMembershipProvider) Membership.Provider).CreateUser(registerViewModel.Name,
                        registerViewModel.Password);

                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(registerViewModel.Name, false);
                    return RedirectToAction("All", "File");
                }
                ModelState.AddModelError("", "Error registration");
            }
            return View(registerViewModel);
        }

        [AllowAnonymous]
        [ActionName("Profile")]
        public ActionResult GetProfile(string name)
        {
            var user = userService.GetUserEntity(name).ToMvcUser();

            return View(user);
        }

        [Authorize]
        public ActionResult ChangePassword(int id)
        {
            try
            {
                var user = userService.GetUserEntity(id);
                if (User.Identity.Name == user.Name)
                    return View(new ChangePasswordViewModel() { Id = user.Id });
            }
            catch (NullReferenceException)
            {
                var e = new HttpException(404, "There is no such user");
                throw e;
            }

            var exception = new HttpException(403, "You cannot edit this user");
            throw exception;
        }

        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = userService.GetUserEntity(changePasswordViewModel.Id);

                if (user == null)
                {
                    ModelState.AddModelError("", "User with this name doesn't exist.");
                    return View(changePasswordViewModel);
                }

                var flag = ((CustomMembershipProvider) Membership.Provider).ChangePassword(user.Name,
                    changePasswordViewModel.Password);

                if (flag)
                {
                    return RedirectToAction("Profile", "Account", new { name = user.Name });
                }
                ModelState.AddModelError("", "Error changing password");
            }
            return View(changePasswordViewModel);
        }
    }
}