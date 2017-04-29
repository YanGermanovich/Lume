using DAL.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DalToOrmMappers
{
    public static class StockImageMapper
    {
        public static stock_and_image ToOrm(this DalStockImage stImg)
        {
            if(stImg == null)
            {
                throw new ArgumentNullException(nameof(stImg));
            }
            return new stock_and_image()
            {
                id_stock_and_image = stImg.Id,
                id_image = stImg.Id_Image,
                id_stock = stImg.Id_Stock
            };
        }

        public static DalStockImage ToDal(this stock_and_image stImg)
        {
            if (stImg == null)
            {
                throw new ArgumentNullException(nameof(stImg));
            }
            return new DalStockImage()
            {
                Id = stImg.id_stock_and_image,
                Id_Image = stImg.id_image,
                Id_Stock = stImg.id_stock
            };
        }
    }
}
