using DAL.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DalToOrmMappers
{
    public static class CountryMapper
    {
        public static country ToOrm(this DalCountry cntry)
        {
            if (cntry == null)
                throw new ArgumentNullException(nameof(cntry));
            return new country()
            {
                id_country = cntry.Id,
                country_name = cntry.Name
            };
        }

        public static DalCountry ToDal(this country cntry)
        {
            if (cntry == null)
                throw new ArgumentNullException(nameof(cntry));
            return new DalCountry()
            {
                Id = cntry.id_country,
                Name = cntry.country_name
            };
        }

    }
}
