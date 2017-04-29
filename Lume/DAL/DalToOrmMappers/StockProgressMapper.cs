using DAL.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DalToOrmMappers
{
    public static class StockProgressMapper
    {
        public static stock_progress ToOrm(this DalStockProgress stProg)
        {
            if(stProg == null)
            {
                throw new ArgumentNullException(nameof(stProg));
            }
            return new stock_progress()
            {
                id_stock_progress = stProg.Id,
                id_stock_and_Image = stProg.Id_StockImage,
                id_user = stProg.Id_User,
                is_scanned = stProg.IsScanndes
            };
        }

        public static DalStockProgress ToDal(this stock_progress stProg)
        {
            if (stProg == null)
            {
                throw new ArgumentNullException(nameof(stProg));
            }
            return new DalStockProgress()
            {
                Id = stProg.id_stock_progress,
                Id_StockImage = stProg.id_stock_and_Image,
                Id_User = stProg.id_user,
                IsScanndes = stProg.is_scanned
            };
        }
    }
}
