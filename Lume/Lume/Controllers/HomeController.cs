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
        IService<BllHistory> _historyService;

        public HomeController(IService<BllUser> userService, IService<BllImage> imageService, IService<BllHistory> historyService)
        {
            _userService = userService;
            _imageService = imageService;
            _historyService = historyService;
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
            image.E = (double)Convert.ToDecimal(Request.Form["E"].ToString().Replace('.', ','));
            image.N = (double)Convert.ToDecimal(Request.Form["N"].ToString().Replace('.', ','));
            var file = Request.Files[0];
            image.Src = tmpKey;
            image.PublicationDate = DateTime.Now;
            image.Id_Author = _userService.GetFirstByPredicate(u => u.Email == User.Identity.Name).Id;
            _imageService.Create(image.ToBll());
            var imageWithId = _imageService.GetFirstByPredicate(i => i.Src == tmpKey);
            imageWithId.Src = "images/"+ imageWithId.Id + Path.GetExtension(file.FileName);
            _imageService.Update(imageWithId);
            MemoryStream stream = new MemoryStream();
            string location = $"ftp://lume.datacenter.by/public_html/images/{imageWithId.Id}{Path.GetExtension(file.FileName)}";
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

        public ActionResult DeletePhoto(long id)
        {
            var image = _imageService.GetEntitieById(id);
            string location = $"ftp://lume.datacenter.by/public_html/{image.Src}";

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(location);

            request.Credentials = new NetworkCredential("lume", "qweasdzxc123");

            request.Method = WebRequestMethods.Ftp.DeleteFile;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            response.Close();
            _imageService.Delete(image);
            return null;
        }
        public ActionResult AllImages()
        {
            return View();
        }

        public ActionResult GetAllImages()
        {
            return Json(_imageService.GetAllEntities().Select(i => i.ToMvc(_userService, _historyService, User.Identity.Name)), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateImage(long id, string desc)
        {
            var image = _imageService.GetEntitieById(id);
            image.description = desc;
            _imageService.Update(image);
            return null;
        }

        public ActionResult GetPopular()
        {
            var topPopular = _historyService.GetAllEntities().Where(h => (DateTime.Now - h.ScaningDate).Days <= 7)
                                                             .GroupBy(x => x.Id_Image)
                                                             .Select(g => new { Value = g.Key, Count = g.Count() })
                                                             .OrderByDescending(x => x.Count)
                                                             .Take(3)
                                                             .Select(i => _imageService.GetEntitieById(i.Value).ToMvc(_userService,_historyService, User.Identity.Name));
            return Json(topPopular, JsonRequestBehavior.AllowGet);
        }
    }
}
