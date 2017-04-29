using DAL.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DalToOrmMappers
{
    public static class UserStockMapper
    {
        public static user_and_stock ToOrm(this DalUserStock usStock)
        {
            if (usStock == null)
            {
                throw new ArgumentNullException(nameof(usStock));
            }
            return new user_and_stock()
            {
                id_User_and_Stock = usStock.Id,
                id_stock = usStock.Id_Stock,
                id_user = usStock.Id_User,
                stock_progress = usStock.Progress
            };
        }

        public static DalUserStock ToDal(this user_and_stock usStock)
        {
            if (usStock == null)
            {
                throw new ArgumentNullException(nameof(usStock));
            }
            return new DalUserStock()
            {
                Id= usStock.id_User_and_Stock,
                Id_Stock = usStock.id_stock,
                Id_User = usStock.id_user,
                Progress = usStock.stock_progress ?? false
            };
        }
    }
}
