using BLL.Entities;
using BLL.Services_Interface;
using Lume.Infrastructure.Helper;
using Lume.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lume.Infrastructure.Mappers
{
    public static class StockImageMapper
    {
        public static StockImageViewModel ToMvc(this BllStockImage image, List<BllStockProgress> stockProgressService, List<BllImage> imageService, long id_user)
        {
            var sPr = stockProgressService.FirstOrDefault(sP => sP.Id_StockImage == image.Id && sP.Id_User == id_user);
            return new StockImageViewModel()
            {
                Id = image.Id_Image,
                Src = imageService.First(im => im.Id == image.Id_Image).Src,
                isScanned = sPr==null ? false : sPr.IsScannded
            };
        }
    }
}