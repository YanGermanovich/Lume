using DAL.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DalToOrmMappers
{
    public static class StockMapper
    {
        public static stock ToOrm(this DalStock st)
        {
            if (st == null)
                throw new ArgumentNullException(nameof(st));
            return new stock()
            {
                id_stock = st.Id,
                stock_name = st.Name,
                id_author = st.Id_Author,
                description_stock = st.Description,
                date_begin = st.BeginingDate,
                date_end = st.FinishingDate,
                id_stock_type = st.Id_StockType
            };
        }

        public static DalStock ToOrm(this stock st)
        {
            if (st == null)
                throw new ArgumentNullException(nameof(st));
            return new DalStock()
            {
                Id = st.id_stock,
                Name = st.stock_name,
                Id_Author = st.id_author,
                Description = st.description_stock,
                BeginingDate = st.date_begin,
                FinishingDate = st.date_end,
                Id_StockType = st.id_stock_type
            };
        }
    }
}
