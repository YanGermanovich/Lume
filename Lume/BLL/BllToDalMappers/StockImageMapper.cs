using DAL.DTO;
using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BllToDalMappers
{
    public static class StockImageMapper
    {
        public static BllStockImage ToBll(this DalStockImage stImg)
        {
            if(stImg == null)
            {
                throw new ArgumentNullException(nameof(stImg));
            }
            return new BllStockImage()
            {
                Id= stImg.Id,
                Id_Image = stImg.Id_Image,
                Id_Stock = stImg.Id_Stock
            };
        }

        public static DalStockImage ToDal(this BllStockImage stImg)
        {
            if (stImg == null)
            {
                throw new ArgumentNullException(nameof(stImg));
            }
            return new DalStockImage()
            {
                Id = stImg.Id,
                Id_Image = stImg.Id_Image,
                Id_Stock = stImg.Id_Stock
            };
        }
    }
}
