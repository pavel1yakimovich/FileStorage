using System;
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
            return View(userService.GetAllUserEntities().Select(user => user.ToMvcUser()));
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
                if (Membership.ValidateUser(model.Email, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "File");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Wrong password or email");
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
                var anyUser = userService.GetAllUserEntities().Any(u => u.Email.Contains(registerViewModel.Email));

                if (anyUser)
                {
                    ModelState.AddModelError("", "User with this address already registered.");
                    return View(registerViewModel);
                }

                MembershipUser membershipUser =
                    ((CustomMembershipProvider) Membership.Provider).CreateUser(registerViewModel.Email,
                        registerViewModel.Password,
                        registerViewModel.FirstName, registerViewModel.LastName, registerViewModel.About);

                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(registerViewModel.Email, false);
                    return RedirectToAction("Index", "File");
                }
                ModelState.AddModelError("", "Error registration");
            }
            return View(registerViewModel);
        }
    }
}