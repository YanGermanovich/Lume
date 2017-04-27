using DAL.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DalToOrmMappers
{
    public static class HistoryMapper
    {
        public static history ToOrm(this DalHistory hist)
        {
            if (hist == null)
                throw new ArgumentNullException(nameof(hist));
            return new history()
            {
                id_History = hist.Id,
                history_E = hist.E,
                history_N = hist.N,
                Date_scaning = hist.ScaningDate,
                id_image = hist.Id_Image,
                id_user = hist.Id_User
            };
        }
        public static DalHistory ToDal(this history hist)
        {
            if (hist == null)
                throw new ArgumentNullException(nameof(hist));
            return new DalHistory()
            {
                Id = hist.id_History,
                E= hist.history_E,
                N= hist.history_N,
                ScaningDate= hist.Date_scaning,
                Id_Image = hist.id_image,
                Id_User = hist.id_user
            };
        }
    }
}
