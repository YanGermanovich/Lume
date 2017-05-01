using DAL.DTO;
using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BllToBllMappers
{
    public static class CityMapper
    {
        public static BllCity ToBll(this DalCity city)
        {
            if (city == null)
                throw new ArgumentNullException(nameof(city));
            return new BllCity()
            {
                Id = city.Id,
                Name = city.Name,
                Id_Country = city.Id_Country
            };
        }
        public static DalCity ToDal(this BllCity city)
        {
            if (city == null)
                throw new ArgumentNullException(nameof(city));
            return new DalCity()
            {
                Id = city.Id,
                Name = city.Name,
                Id_Country = city.Id_Country
            };
        }
    }
}
