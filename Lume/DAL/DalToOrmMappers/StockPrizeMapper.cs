using DAL.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DalToOrmMappers
{
    public static class StockPrizeMapper
    {
        public static stock_prize ToOrm(this DalStockPrize stPr)
        {
            if (stPr == null)
            {
                throw new ArgumentNullException(nameof(stPr));
            }
            return new stock_prize()
            {
                id_stock_prize = stPr.Id,
                id_prize = stPr.Id_Prize,
                Stock_id_stock = stPr.Id_Stock
            };
        }

        public static DalStockPrize ToDal(this stock_prize stPr)
        {
            if (stPr == null)
            {
                throw new ArgumentNullException(nameof(stPr));
            }
            return new DalStockPrize()
            {
                Id = stPr.id_stock_prize,
                Id_Prize = stPr.id_prize,
                Id_Stock = stPr.Stock_id_stock
            };
        }
    }
}
