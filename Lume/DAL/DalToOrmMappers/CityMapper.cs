using DAL.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DalToOrmMappers
{
    public static class CityMapper
    {
        public static city ToOrm(this DalCity city)
        {
            if (city == null)
                throw new ArgumentNullException(nameof(city));
            return new city()
            {
                id_city = city.Id,
                city_name = city.Name,
                id_country = city.Id_Country
            };
        }
        public static DalCity ToDal(this city city)
        {
            if (city == null)
                throw new ArgumentNullException(nameof(city));
            return new DalCity()
            {
                Id = city.id_city,
                Name= city.city_name,
                Id_Country= city.id_country
            };
        }
    }
}
