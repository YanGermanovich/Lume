using DAL.DTO;
using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BllToDalMappers
{
    public static class PrizeTypeMapper
    {
        public static BllPrizeType ToBll(this DalPrizeType prType)
        {
            if(prType == null)
            {
                throw new ArgumentNullException(nameof(prType));
            }
            return new BllPrizeType()
            {
                Id = prType.Id,
                Name = prType.Name
            };
        }

        public static DalPrizeType ToDal(this BllPrizeType prType)
        {
            if (prType == null)
            {
                throw new ArgumentNullException(nameof(prType));
            }
            return new DalPrizeType()
            {
                Id = prType.Id,
                Name = prType.Name
            };
        }
    }
}
