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
        private readonly IFileService service;

        public FileController(IFileService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            return View(service.GetAllPublicFileEntities().Select(file => file.ToMvcFile()));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
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

            service.CreateFile(file.ToBllFile());

            return RedirectToAction("Index");

        }

        public FileResult GetFile(int fileId)
        {
            var file = service.GetFileEntity(fileId).ToMvcFile();
            return File(file.Content, file.Type, file.Name);
        }
    }
}