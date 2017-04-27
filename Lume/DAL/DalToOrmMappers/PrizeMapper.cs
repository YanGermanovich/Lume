using DAL.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DalToOrmMappers
{
    public static class PrizeMapper
    {
        public static prize ToOrm(this DalPrize pr)
        {
            if (pr == null)
                throw new ArgumentNullException(nameof(pr));
            return new prize()
            {
                id_prize = pr.Id,
                id_type_prize = pr.Id_PrizeType,
                prize_description = pr.Description,
                prize_data = pr.Data
            };
        }

        public static DalPrize ToDal(this prize pr)
        {
            if (pr == null)
                throw new ArgumentNullException(nameof(pr));
            return new DalPrize()
            {
                Id = pr.id_prize,
                Id_PrizeType= pr.id_type_prize,
                Description= pr.prize_description,
                Data= pr.prize_data
            };
        }
    }
}
