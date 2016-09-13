using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Services;
using Microsoft.Ajax.Utilities;
using MVCUI.Infrastructure.Mappers;
using MVCUI.ViewModels;
using MVCUI.ViewModels.File;

namespace MVCUI.Controllers
{
    public class FileController : Controller
    {
        private readonly IUserService userService;
        private readonly IFileService fileService;

        public FileController(IFileService fileService, IUserService userService)
        {
            this.userService = userService;
            this.fileService = fileService;
        }

        public ActionResult All(int page = 1)
        {
            var list = User.IsInRole("Admin") ? fileService.GetAllFileEntities().Select(file => file.ToMvcFile()) :
                fileService.GetAllPublicFileEntities().Select(file => file.ToMvcFile());

            int pageSize = 3; // количество объектов на страницу
            IEnumerable<FileViewModel> filesPerPages = list.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = list.Count() };
            IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, Files = filesPerPages };

            if (Request.IsAjaxRequest())
            {
                return PartialView(ivm);
            }

            return View(ivm);
        }

        public PartialViewResult List(int page = 1)
        {
            var list = User.IsInRole("Admin") ? fileService.GetAllFileEntities().Select(file => file.ToMvcFile()) :
                fileService.GetAllPublicFileEntities().Select(file => file.ToMvcFile());

            int pageSize = 3; // количество объектов на страницу
            IEnumerable<FileViewModel> filesPerPages = list.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = list.Count() };
            IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, Files = filesPerPages };
            return PartialView("_FileTable",ivm);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(FileViewModel file, HttpPostedFileBase uploadFile)
        {
            byte[] fileData;

            using (var binaryReader = new BinaryReader(uploadFile.InputStream))
            {
                fileData = binaryReader.ReadBytes(uploadFile.ContentLength);
            }

            file.Name = uploadFile.FileName;
            file.Content = fileData;
            file.Date = DateTime.Now;
            file.Type = uploadFile.ContentType;
            file.User = User.Identity.Name;
            file.UserId = userService.GetUserEntity(User.Identity.Name).Id;
            
            fileService.CreateFile(file.ToBllFile());

            return RedirectToAction("All");

        }
        
        public FileResult GetFile(int fileId)
        {
            var file = fileService.GetFileEntity(fileId).ToMvcFile();
            return File(file.Content, file.Type, file.Name);
        }

        public ActionResult UserFiles(string name, int page = 1)
        {
            ViewBag.Name = name;
            var list = User.IsInRole("Admin") || User.Identity.Name == name ? 
                fileService.GetAllFileEntitiesOfUser(name).Select(file => file.ToMvcFile()) :
                fileService.GetAllPublicFileEntitiesOfUser(name).Select(file => file.ToMvcFile());

            int pageSize = 3; // количество объектов на страницу
            IEnumerable<FileViewModel> filesPerPages = list.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = list.Count() };
            IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, Files = filesPerPages };
            return View(ivm);
        }

        [Authorize]
        public ActionResult Delete(int fileId)
        {
            try
            {
                var file = fileService.GetFileEntity(fileId).ToMvcFile();
                if (User.IsInRole("Admin") || User.Identity.Name == file.User)
                    return View(file);
            }
            catch (NullReferenceException)
            {
                var e = new HttpException(404, "There is no such file");
                throw e;
            }

            var exception = new HttpException(403, "You cannot delete this file");
            throw exception;
        }

        [HttpPost]
        [Authorize]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int fileId)
        {
            fileService.DeleteFile(fileService.GetFileEntity(fileId));

            return RedirectToAction("All");
        }

        [Authorize]
        public ActionResult Edit(int fileId)
        {
            try
            {
                var file = fileService.GetFileEntity(fileId).ToMvcFile();
                if (User.IsInRole("Admin") || User.Identity.Name == file.User)
                    return View(file);
            }
            catch (NullReferenceException)
            {
                var e = new HttpException(404, "There is no such file");
                throw e;
            }

            var exception = new HttpException(403, "You cannot edit this file");
            throw exception;
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(FileViewModel file)
        {
            file.Date = DateTime.Now;
            fileService.UpdateFile(file.ToBllFile());

            return RedirectToAction("All");
        }
    }
}