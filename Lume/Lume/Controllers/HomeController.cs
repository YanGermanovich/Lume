using BLL.Entities;
using BLL.Services_Interface;
using Lume.Infrastructure.Mappers;
using Lume.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Lume.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        IService<BllUser> _userService;
        IService<BllImage> _imageService;

        public HomeController(IService<BllUser> userService, IService<BllImage> imageService)
        {
            _userService = userService;
            _imageService = imageService;
        }
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult UploadPhoto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadPhoto(ImageViewModel image)
        {
            string tmpKey = Guid.NewGuid().ToString();
            var file = Request.Files[0];
            image.Src = tmpKey;
            _imageService.Create(image.ToBll());
            var imageWithId = _imageService.GetFirstByPredicate(i => i.Src == tmpKey);
            imageWithId.Src = imageWithId.Id + Path.GetExtension(file.FileName);
            _imageService.Update(imageWithId);
            MemoryStream stream = new MemoryStream();
            string location = $"ftp://lume.datacenter.by/public_html/images/{imageWithId.Src}";
            WebRequest ftpRequest = WebRequest.Create(location);
            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;

            file.InputStream.CopyTo(stream);


            ftpRequest.Credentials = new NetworkCredential("lume", "qweasdzxc123");

            using (Stream requestStream = ftpRequest.GetRequestStream())
            {
                requestStream.Write(stream.GetBuffer(), 0, (int)stream.Length);
                requestStream.Close();
            }
            return View();
        }

    }
}
