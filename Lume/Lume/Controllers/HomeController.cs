using BLL.Entities;
using BLL.Services_Interface;
using Lume.Infrastructure.Helper;
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
        IService<BllStock> _stockService;
        IService<BllStockImage> _stockImageService;
        IService<BllStockPrize> _stockPrizeService;
        IService<BllStockProgress> _stockProgressService;
        IService<BllUserStock> _userStockService;
        IService<BllStockType> _stockTypeService;
        IService<BllAvatar> _avatarsService;
        IService<BllPrize> _prizeService;

        public HomeController(IService<BllUser> userService, IService<BllImage> imageService, IService<BllHistory> historyService, 
                             IService<BllStock> stockService, IService<BllStockImage> stockImageService, IService<BllStockProgress> stockProgressService,
                             IService<BllUserStock> userStockService, IService<BllStockType> stockType, IService<BllAvatar> avatarsService,
                             IService<BllPrize> prizeService, IService<BllStockPrize> stockPrizeService)
        {
            _userService = userService;
            _imageService = imageService;
            _historyService = historyService;
            _stockService = stockService;
            _stockImageService = stockImageService;
            _stockProgressService = stockProgressService;
            _userStockService = userStockService;
            _stockTypeService = stockType;
            _avatarsService = avatarsService;
            _prizeService = prizeService;
            _stockPrizeService = stockPrizeService;
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

        public ActionResult Actions()
        {
            return View();
        }

        public ActionResult GetAllStocks()
        {
            long myId = _userService.GetFirstByPredicate(u => u.Email == User.Identity.Name).Id;
            var st = _stockService.GetAllEntities().ToList().Select(s => s.ToMvc(_userService, _imageService, _stockImageService, 
                                                                                 _stockTypeService, _historyService,_avatarsService,
                                                                                 _userStockService, _stockProgressService, 
                                                                                 _stockPrizeService,_prizeService, User.Identity.Name)).ToList();
            return Json(st, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateStock(long id, string desc, string name)
        {
            var stock = _stockService.GetEntitieById(id);
            stock.Description= desc;
            stock.Name = name;
            _stockService.Update(stock);
            return null;
        }

        public ActionResult NewAction()
        {
            return View();
        }

        public ActionResult UploadAction(StockViewModel stock)
        {
            if (stock.stockType!=null)
            {
                _stockTypeService.Create(new BllStockType() { Name = stock.stockType });
                stock.id_stockType = _stockTypeService.GetFirstByPredicate(ValueCompileVisitor.Convert<BllStockType>(sT => sT.Name == stock.stockType)).Id;
            }
            var myId = _userService.GetFirstByPredicate(ValueCompileVisitor.Convert<BllUser>(u => u.Email == User.Identity.Name)).Id;
            stock.id_author = myId;
            _stockService.Create(stock.ToBll());
            var stock_id = _stockService.GetFirstByPredicate(s => s.Name == stock.Name && s.BeginingDate == stock.BeginData).Id;
            foreach (var imgage in stock.Image)
            {
                _stockImageService.Create(new BllStockImage() { Id_Image = imgage.Id, Id_Stock = stock_id });
            }
            return null;

        }

        public ActionResult GetAllTypes()
        {
            return Json(_stockTypeService.GetAllEntities().Select(sT => new { Name = sT.Name, Id = sT.Id }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllMyImages()
        {
            var myId = _userService.GetFirstByPredicate(ValueCompileVisitor.Convert<BllUser>(u => u.Email == User.Identity.Name)).Id;
            return Json(_imageService.GetAllByPredicate(ValueCompileVisitor.Convert<BllImage>(im => im.Id_Author == myId)).Select(im => new { Src = im.Src, Id = im.Id }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult TakePart(long id)
        {
            long myId = _userService.GetFirstByPredicate(u => u.Email == User.Identity.Name).Id;
            var stock = _stockService.GetEntitieById(id);
            var allImages = _stockImageService.GetAllByPredicate(sI => sI.Id_Stock == id);
            foreach(var im in allImages)
            {
                _stockProgressService.Create(new BllStockProgress() { Id_StockImage = im.Id, Id_User = myId, IsScannded = false });
            }
            _userStockService.Create(new BllUserStock() { Id_Stock = id, Id_User = myId, Progress = false });
            return null;
        }
    }
}
