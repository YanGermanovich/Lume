using BLL.Entities;
using BLL.Services_Interface;
using Lume.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Lume.Infrastructure.Helper;

namespace Lume.Infrastructure.Mappers
{
    public static class ImageMapper
    {
        public static BllImage ToBll(this ImageViewModel image)
        {
            return new BllImage()
            {
                Id = image.Id,
                Id_Properies = image.Id_Properies,
                Id_Author = image.Id_Author,
                Id_ImageCategory = image.Id_ImageCategory,
                Id_Event = image.Id_Event,
                PublicationDate = image.PublicationDate,
                description = image.description,
                Src = image.Src,
                N = image.N,
                E = image.E,
                IsConfirmed = image.IsConfirmed
            };
        }
        public static ImageViewModel ToMvc(this BllImage image, IService<BllUser> _userService, IService<BllHistory> _historyService, string Email)
        {
            return new ImageViewModel()
            {
                Id = image.Id,
                Id_Properies = image.Id_Properies,
                Id_Author = image.Id_Author,
                Author_Name = _userService.GetEntitieById(image.Id_Author).UserName,
                ScanningCount = GetScanningCount(_historyService,image.Id),
                Id_ImageCategory = image.Id_ImageCategory,
                Id_Event = image.Id_Event,
                PublicationDate = image.PublicationDate,
                description = image.description,
                isMy = _userService.GetEntitieById(image.Id_Author).Email == Email,
                Src = image.Src,
                N = image.N,
                E = image.E,
                IsConfirmed = image.IsConfirmed
            };
        }

        public static long GetScanningCount(IService<BllHistory> _historyService, long? id)
        {
            Expression<Func<BllHistory, bool>> f = h => h.Id_Image == id;
            return _historyService.GetAllByPredicate(ValueCompileVisitor.Convert(f)).Count();
        }
    }
}