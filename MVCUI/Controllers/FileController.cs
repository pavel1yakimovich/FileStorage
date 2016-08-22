using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Services;
using MVCUI.Infrastructure.Mappers;
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

        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
                return View(fileService.GetAllFileEntities().Select(file => file.ToMvcFile()));
            return View(fileService.GetAllPublicFileEntities().Select(file => file.ToMvcFile()));
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(UploadViewModel file, HttpPostedFileBase uploadFile)
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
            file.User = userService.GetUserEntity(User.Identity.Name).ToMvcUser();
            
            fileService.CreateFile(file.ToBllFile());

            return RedirectToAction("Index");

        }
        
        public FileResult GetFile(int fileId)
        {
            var file = fileService.GetFileEntity(fileId).ToMvcFile();
            return File(file.Content, file.Type, file.Name);
        }
    }
}