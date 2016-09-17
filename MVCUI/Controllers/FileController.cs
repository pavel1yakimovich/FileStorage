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
        private const int pageSize = 3;

        public FileController(IFileService fileService, IUserService userService)
        {
            this.userService = userService;
            this.fileService = fileService;
        }

        public ActionResult All(int page = 1)
        {
            var list = User.IsInRole("Admin") ? fileService.GetAllFileEntities().Select(file => file.ToMvcFile()) :
                fileService.GetAllPublicFileEntities().Select(file => file.ToMvcFile());
            
            var ivm = GetIvm(list, page);

            if (Request.IsAjaxRequest())
            {
                return PartialView(ivm);
            }

            return View(ivm);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Upload( FileViewModel file = null, HttpPostedFileBase uploadFile = null)
        {
            byte[] fileData;
            if (uploadFile == null)
            {
                file = new FileViewModel();
                using (var binaryReader = new BinaryReader(Request.InputStream))
                {
                    fileData = binaryReader.ReadBytes(Request.ContentLength);
                }

                file.Name = Request.Headers["X-FILE-NAME"];
                file.Type = Request.Headers["X-FILE-TYPE"];
                file.IsPublic = (Request.Headers["X-FILE-ISPUBLIC"] == "true");
                file.Description = Request.Headers["X-FILE-Description"];
            }
            else
            {
                using (var binaryReader = new BinaryReader(uploadFile.InputStream))
                {
                    fileData = binaryReader.ReadBytes(uploadFile.ContentLength);
                }
                file.Name = uploadFile.FileName;
                file.Type = uploadFile.ContentType;
            }

            file.Content = fileData;
            file.UserId = userService.GetUserEntity(User.Identity.Name).Id;
            file.User = User.Identity.Name;
            file.Date = DateTime.Now;
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
            ViewBag.Id = userService.GetUserEntity(name).Id;

            var list = User.IsInRole("Admin") || User.Identity.Name == name ?
                fileService.GetAllFileEntitiesOfUser(name).Select(file => file.ToMvcFile()) :
                fileService.GetAllPublicFileEntitiesOfUser(name).Select(file => file.ToMvcFile());

            var ivm = GetIvm(list, page);

            if (Request.IsAjaxRequest())
            {
                return PartialView(ivm);
            }

            return View(ivm);
        }

        //public ActionResult Search(string str, int page = 1)
        //{
        //    var list = User.IsInRole("Admin")
        //        ? fileService.GetAllFileEntities()
        //            .Where(x => x.Name.Contains(str) || x.Description.Contains(str))
        //            .ToList().Select(file => file.ToMvcFile())
        //        : fileService.GetAllPublicFileEntities()
        //            .Where(x => x.Name.Contains(str) || x.Description.Contains(str))
        //            .ToList().Select(file => file.ToMvcFile());

        //    var ivm = GetIvm(list, page);

        //    return PartialView(ivm);
        //}
        
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

        private IndexViewModel<FileViewModel> GetIvm(IEnumerable<FileViewModel> list, int page)
        {
            IEnumerable<FileViewModel> filesPerPages = list.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = list.Count() };
            IndexViewModel<FileViewModel> ivm = new IndexViewModel<FileViewModel> { PageInfo = pageInfo, Items = filesPerPages };

            return ivm;
        }
    }
}