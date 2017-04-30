using DAL.DTO;
using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BllToDalMappers
{
    public static class StockProgressMapper
    {
        public static BllStockProgress ToBll(this DalStockProgress stProg)
        {
            if(stProg == null)
            {
                throw new ArgumentNullException(nameof(stProg));
            }
            return new BllStockProgress()
            {
                Id = stProg.Id,
                Id_StockImage = stProg.Id_StockImage,
                Id_User = stProg.Id_User,
                IsScannded = stProg.IsScannded
            };
        }

        public static DalStockProgress ToDal(this BllStockProgress stProg)
        {
            if (stProg == null)
            {
                throw new ArgumentNullException(nameof(stProg));
            }
            return new DalStockProgress()
            {
                Id = stProg.Id,
                Id_StockImage = stProg.Id_StockImage,
                Id_User = stProg.Id_User,
                IsScannded = stProg.IsScannded
            };
        }
    }
}
