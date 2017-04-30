using DAL.DTO;
using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BllToDalMappers
{
    public static class PrizeMapper
    {
        public static BllPrize ToBll(this DalPrize pr)
        {
            if (pr == null)
                throw new ArgumentNullException(nameof(pr));
            return new BllPrize()
            {
                Id = pr.Id,
                Id_PrizeType = pr.Id_PrizeType,
                Description = pr.Description,
                Data = pr.Data
            };
        }

        public static DalPrize ToDal(this BllPrize pr)
        {
            if (pr == null)
                throw new ArgumentNullException(nameof(pr));
            return new DalPrize()
            {
                Id = pr.Id,
                Id_PrizeType = pr.Id_PrizeType,
                Description = pr.Description,
                Data = pr.Data
            };
        }
    }
}
