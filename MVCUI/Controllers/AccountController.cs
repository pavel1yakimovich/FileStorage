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
    public class AccountController : Controller
    {
        private readonly IUserService service;

        public AccountController(IUserService service)
        {
            this.service = service;
        }

        [ActionName("Index")]
        public ActionResult GetAllUsers()
        {
            return View(service.GetAllUserEntities().Select(user => user.ToMvcUser()));
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
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
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var anyUser = service.GetAllUserEntities().Any(u => u.Email.Contains(registerViewModel.Email));

                if (anyUser)
                {
                    ModelState.AddModelError("", "User with this address already registered.");
                    return View(registerViewModel);
                }
                
                MembershipUser membershipUser = ((CustomMembershipProvider)Membership.Provider).CreateUser(registerViewModel.Email, registerViewModel.Password, 
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