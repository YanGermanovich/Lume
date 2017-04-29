using DAL.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DalToOrmMappers
{
    public static class StockTypeMapper
    {
        public static stock_type ToOrm(this DalStockType stType)
        {
            if(stType == null)
            {
                throw new ArgumentNullException(nameof(stType));
            }
            return new stock_type()
            {
                id_stock_type = stType.Id,
                stock_type1 = stType.Name
            };
        }

        public static DalStockType ToDal(this stock_type stType)
        {
            if (stType == null)
            {
                throw new ArgumentNullException(nameof(stType));
            }
            return new DalStockType()
            {
                Id = stType.id_stock_type,
                Name= stType.stock_type1
            };
        }
    }
}
