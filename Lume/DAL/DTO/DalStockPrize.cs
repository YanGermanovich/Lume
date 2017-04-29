using CustomExpressionVisitor;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class DalStockPrize : IEntity
    {
        [CustomAttributeMapper("id_stock_prize")]
        public long Id { get; set; }

        [CustomAttributeMapper("Stock_id_stock")]
        public long Id_Stock { get; set; }

        [CustomAttributeMapper("id_prize")]
        public long Id_Prize { get; set; }
    }
}
