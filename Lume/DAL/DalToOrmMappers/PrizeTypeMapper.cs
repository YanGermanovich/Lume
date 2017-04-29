using DAL.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DalToOrmMappers
{
    public static class PrizeTypeMapper
    {
        public static type_of_prize ToOrm(this DalPrizeType prType)
        {
            if(prType == null)
            {
                throw new ArgumentNullException(nameof(prType));
            }
            return new type_of_prize()
            {
                id_type = prType.Id,
                prize_type = prType.Name
            };
        }

        public static DalPrizeType ToDal(this type_of_prize prType)
        {
            if (prType == null)
            {
                throw new ArgumentNullException(nameof(prType));
            }
            return new DalPrizeType()
            {
                Id = prType.id_type,
                Name = prType.prize_type
            };
        }
    }
}
