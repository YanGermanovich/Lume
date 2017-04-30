using DAL.DTO;
using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BllToDalMappers
{
    public static class StockMapper
    {
        public static BllStock ToBll(this DalStock st)
        {
            if (st == null)
                throw new ArgumentNullException(nameof(st));
            return new BllStock()
            {
                Id = st.Id,
                Name = st.Name,
                Id_Author = st.Id_Author,
                Description = st.Description,
                BeginingDate = st.BeginingDate,
                FinishingDate = st.FinishingDate,
                Id_StockType = st.Id_StockType
            };
        }

        public static DalStock ToDal(this BllStock st)
        {
            if (st == null)
                throw new ArgumentNullException(nameof(st));
            return new DalStock()
            {
                Id = st.Id,
                Name = st.Name,
                Id_Author = st.Id_Author,
                Description = st.Description,
                BeginingDate = st.BeginingDate,
                FinishingDate = st.FinishingDate,
                Id_StockType = st.Id_StockType
            };
        }
    }
}
