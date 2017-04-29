using DAL.Interface;
using DAL.DalToOrmMappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomExpressionVisitor;
using ORM;

namespace DAL.DTO
{
    public class DalStock : IEntity
    {
        [CustomAttributeMapper("id_stock")]
        public long Id { get; set; }

        [CustomAttributeMapper("stock_name")]
        public string Name { get; set; }

        [CustomAttributeMapper("id_author")]
        public long Id_Author { get; set; }

        [CustomAttributeMapper("id_stock_type")]
        public long Id_StockType { get; set; }

        [CustomAttributeMapper("description_stock")]
        public string Description { get; set; }

        [CustomAttributeMapper("date_begin")]
        public DateTime BeginingDate { get; set; }

        [CustomAttributeMapper("date_end")]
        public DateTime? FinishingDate { get; set; }
    }
}
