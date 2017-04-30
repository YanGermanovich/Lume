using DAL.DTO;
using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BllToDalMappers
{
    public static class CountryMapper
    {
        public static BllCountry ToBll(this DalCountry cntry)
        {
            if (cntry == null)
                throw new ArgumentNullException(nameof(cntry));
            return new BllCountry()
            {
                Id = cntry.Id,
                Name = cntry.Name
            };
        }

        public static DalCountry ToDal(this BllCountry cntry)
        {
            if (cntry == null)
                throw new ArgumentNullException(nameof(cntry));
            return new DalCountry()
            {
                Id = cntry.Id,
                Name = cntry.Name
            };
        }

    }
}
