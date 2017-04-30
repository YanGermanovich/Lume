using DAL.DTO;
using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BllToDalMappers
{
    public static class HistoryMapper
    {
        public static BllHistory ToBll(this DalHistory hist)
        {
            if (hist == null)
            {
                throw new ArgumentNullException(nameof(hist));
            }
            return new BllHistory()
            {
                Id = hist.Id,
                E = hist.E,
                N = hist.N,
                ScaningDate = hist.ScaningDate,
                Id_Image = hist.Id_Image,
                Id_User = hist.Id_User
            };
        }
        public static DalHistory ToDal(this BllHistory hist)
        {
            if (hist == null)
            {
                throw new ArgumentNullException(nameof(hist));
            }
            return new DalHistory()
            {
                Id = hist.Id,
                E = hist.E,
                N = hist.N,
                ScaningDate = hist.ScaningDate,
                Id_Image = hist.Id_Image,
                Id_User = hist.Id_User
            };
        }
    }
}
