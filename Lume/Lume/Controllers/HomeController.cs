using BLL.Entities;
using BLL.Services_Interface;
using Lume.Infrastructure.Helper;
using Lume.Infrastructure.Mappers;
using Lume.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
        IService<BllPrizeType> _prizeTypeService;
        IService<BllEvent> _eventService;
        IService<BllDataType> _dataTypeService;

        public HomeController(IService<BllUser> userService, IService<BllImage> imageService, IService<BllHistory> historyService, 
                             IService<BllStock> stockService, IService<BllStockImage> stockImageService, IService<BllStockProgress> stockProgressService,
                             IService<BllUserStock> userStockService, IService<BllStockType> stockType, IService<BllAvatar> avatarsService,
                             IService<BllPrize> prizeService, IService<BllStockPrize> stockPrizeService, IService<BllPrizeType> prizeTypeService,
                             IService<BllEvent> eventService, IService<BllDataType> dataTypeService)
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
            _prizeTypeService = prizeTypeService;
            _eventService = eventService;
            _dataTypeService = dataTypeService;
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
            var ev = JsonConvert.DeserializeObject<EventViewModel>(Request.Form["Event"]);
            if (ev.Id != -1)
            {
                image.Id_Event = ev.Id;
            }
            else
            {
                if (ev.TypeId != -1)
                {
                    _eventService.Create(new BllEvent() { Source = ev.Data, Id_DataType = ev.TypeId });
                }
                else
                {
                    var dt = _dataTypeService.GetAllEntities().FirstOrDefault(dT => dT.Name == ev.TypeData);
                    if (dt == null)
                    {
                        _dataTypeService.Create(new BllDataType() { Name = ev.TypeData });
                        ev.TypeId = _dataTypeService.GetAllEntities().Last(dT => dT.Name == ev.TypeData).Id;
                    }
                    else
                    {
                        ev.TypeId = dt.Id;
                    }
                    _eventService.Create(new BllEvent() { Source = ev.Data, Id_DataType = ev.TypeId });
                }
            }
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
            var allUsers = _userService.GetAllEntities().ToList();
            var history = _historyService.GetAllEntities().ToList();
            long myId = _userService.GetFirstByPredicate(u => u.Email == User.Identity.Name).Id;
            if (allUsers.First(u=>u.Id == myId).Type == "Company")
            {
                return Json(_imageService.GetAllEntities().Select(i => i.ToMvc(allUsers, history, _eventService.GetAllEntities().ToList(), User.Identity.Name)), JsonRequestBehavior.AllowGet);
            }
            return Json(_imageService.GetAllEntities().Where(im => history.Any(h=> h.Id_Image == im.Id && h.Id_User == myId) || im.Id_Author == myId).Select(i => i.ToMvc(allUsers, history, _eventService.GetAllEntities().ToList(), User.Identity.Name)), JsonRequestBehavior.AllowGet);
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
                                                             .Select(i => _imageService.GetEntitieById(i.Value).ToMvc(_userService.GetAllEntities().ToList(),_historyService.GetAllEntities().ToList(), _eventService.GetAllEntities().ToList(), User.Identity.Name));
            return Json(topPopular, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Actions()
        {
            return View();
        }

        public ActionResult GetAllStocks()
        {
            long myId = _userService.GetFirstByPredicate(u => u.Email == User.Identity.Name).Id;
            var st = _stockService.GetAllEntities().ToList().Select(s => s.ToMvc(_userService.GetAllEntities().ToList(), _imageService.GetAllEntities().ToList(), _stockImageService.GetAllEntities().ToList(), 
                                                                                 _stockTypeService.GetAllEntities().ToList(), _historyService.GetAllEntities().ToList(), _avatarsService.GetAllEntities().ToList(),
                                                                                 _userStockService.GetAllEntities().ToList(), _stockProgressService.GetAllEntities().ToList(), 
                                                                                 _stockPrizeService.GetAllEntities().ToList(), _prizeService.GetAllEntities().ToList(), User.Identity.Name)).ToList();
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
            foreach (var prize in stock.prizesToUpload)
            {
                if (prize.Id != -1)
                {
                    _stockPrizeService.Create(new BllStockPrize() { Id_Prize = prize.Id, Id_Stock = stock_id });
                }
                else
                {
                    if (prize.id_PrizeType != -1)
                    {
                        _prizeService.Create(new BllPrize() { Data = prize.Data, Description = prize.Description, Id_PrizeType = prize.id_PrizeType });
                    }
                    else
                    {
                        _prizeTypeService.Create(new BllPrizeType() { Name = prize.PrizeType });
                        prize.id_PrizeType = _prizeTypeService.GetAllEntities().Last(pT => pT.Name == prize.PrizeType).Id;
                        _prizeService.Create(new BllPrize() { Data = prize.Data, Description = prize.Description, Id_PrizeType = prize.id_PrizeType });
                    }
                    prize.Id = _prizeService.GetAllEntities().Last(pr => pr.Data == prize.Data & pr.Description == prize.Description & pr.Id_PrizeType == prize.id_PrizeType).Id;
                    _stockPrizeService.Create(new BllStockPrize() { Id_Prize = prize.Id, Id_Stock = stock_id });
                }

            }
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

        public ActionResult GetAllPrizes()
        {
            return Json(new
            {
                prizes = _prizeService.GetAllEntities().Select(pr => new { Description = pr.Description, Id = pr.Id, Id_PrizeType = pr.Id_PrizeType }),
                types = _prizeTypeService.GetAllEntities().Select(pr => new { Name = pr.Name, Id = pr.Id })
            },
            JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllEvents()
        {
            return Json(new
            {
                events = _eventService.GetAllEntities().Select(ev => new { Data = ev.Source, Id = ev.Id, TypeId = ev.Id_DataType }),
                types = _dataTypeService.GetAllEntities().Select(dt => new { Data = dt.Name, Id = dt.Id })
            },
           JsonRequestBehavior.AllowGet);
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

        public ActionResult EditProfile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditProfile(UserViewModel user, string NewPassword)
        {
            if(Membership.ValidateUser(user.Email, user.Password))
            {
                user.Password = NewPassword ?? user.Password;
                _userService.Update(user.ToBll());
                return Json(new { success = "Информация обновлена", Url = Url.Action("AllImages", "Home") }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Error="Неправильный логин или пароль" },JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetUser()
        {
            var myId = _userService.GetFirstByPredicate(ValueCompileVisitor.Convert<BllUser>(u => u.Email == User.Identity.Name)).Id;
            return Json(_userService.GetEntitieById(myId).ToUi(), JsonRequestBehavior.AllowGet);
        }
    }
}
