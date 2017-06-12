using BLL.Entities;
using BLL.Services_Interface;
using Lume.Infrastructure.Helper;
using Lume.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Lume.Infrastructure.Mappers
{
    public static class StockUserMapper
    {
        public static StockUserViewModel ToMvc(this BllUser user, List<BllStockProgress> stockProgressService, List<BllStockImage> stockImageService, long stock_id)
        {
            return new StockUserViewModel()
            {
                Email = user.Email,
                Scanned = stockProgressService.Where(uS => stockImageService.First(sI => sI.Id ==  uS.Id_StockImage).Id_Stock == stock_id & uS.Id_User == user.Id & uS.IsScannded).Count()
            };
        }
        

    }
}