using BLL.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Entities;
using MVCUI.Infrastructure.Mappers;
using MVCUI.ViewModels;

namespace MVCUI.Controllers
{
    public class HomeController : Controller
    {
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}

        private readonly IUserService service;

        public HomeController(IUserService service)
        {
            this.service = service;
        }

        [ActionName("Index")]
        public ActionResult GetAllUsers()
        {
            return View(service.GetAllUserEntities().Select(user => user.ToMvcUser()));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel userViewModel)
        {
            service.CreateUser(userViewModel.ToBllUser());
            return RedirectToAction("Index");
        }

        ////GET-запрос к методу Delete несет потенциальную уязвимость!
        //[HttpGet]
        //public ActionResult Delete(int id = 0)
        //{
        //    UserEntity user = service.GetUserEntity(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(user.ToMvcUser());
        //}

        ////Post/Redirect/Get (PRG) — модель поведения веб-приложений, используемая
        ////разработчиками для защиты от повторной отправки данных веб-форм
        ////(Double Submit Problem)
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(UserEntity user)
        //{
        //    service.DeleteUser(user);
        //    return RedirectToAction("Index");
    }

    //etc.
}