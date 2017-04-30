using DAL.DTO;
using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BllToDalMappers
{
    public static class UserStockMapper
    {
        public static BllUserStock ToBll(this DalUserStock usStock)
        {
            if (usStock == null)
            {
                throw new ArgumentNullException(nameof(usStock));
            }
            return new BllUserStock()
            {
                Id = usStock.Id,
                Id_Stock = usStock.Id_Stock,
                Id_User = usStock.Id_User,
                Progress = usStock.Progress
            };
        }

        public static DalUserStock ToDal(this BllUserStock usStock)
        {
            if (usStock == null)
            {
                throw new ArgumentNullException(nameof(usStock));
            }
            return new DalUserStock()
            {
                Id= usStock.Id,
                Id_Stock = usStock.Id_Stock,
                Id_User = usStock.Id_User,
                Progress = usStock.Progress
            };
        }
    }
}
