using DAL.DTO;
using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BllToDalMappers
{
    public static class StockTypeMapper
    {
        public static BllStockType ToBll(this DalStockType stType)
        {
            if(stType == null)
            {
                throw new ArgumentNullException(nameof(stType));
            }
            return new BllStockType()
            {
                Id = stType.Id,
                Name = stType.Name
            };
        }

        public static DalStockType ToDal(this BllStockType stType)
        {
            if (stType == null)
            {
                throw new ArgumentNullException(nameof(stType));
            }
            return new DalStockType()
            {
                Id = stType.Id,
                Name = stType.Name
            };
        }
    }
}
