using DAL.DTO;
using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BllToDalMappers
{
    public static class StockPrizeMapper
    {
        public static BllStockPrize ToBll(this DalStockPrize stPr)
        {
            if (stPr == null)
            {
                throw new ArgumentNullException(nameof(stPr));
            }
            return new BllStockPrize()
            {
                Id = stPr.Id,
                Id_Prize = stPr.Id_Prize,
                Id_Stock = stPr.Id_Stock
            };
        }

        public static DalStockPrize ToDal(this BllStockPrize stPr)
        {
            if (stPr == null)
            {
                throw new ArgumentNullException(nameof(stPr));
            }
            return new DalStockPrize()
            {
                Id = stPr.Id,
                Id_Prize = stPr.Id_Prize,
                Id_Stock = stPr.Id_Stock
            };
        }
    }
}
