using CustomExpressionVisitor;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class DalStockType : IEntity
    {
        [CustomAttributeMapper("id_stock_type")]
        public long Id { get; set; }

        [CustomAttributeMapper("stock_type1")]
        public string Name { get; set; }
    }
}
