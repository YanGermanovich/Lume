using CustomExpressionVisitor;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class DalStockImage : IEntity
    {
        [CustomAttributeMapper("id_stock_and_image")]
        public long Id { get; set; }

        [CustomAttributeMapper("id_image")]
        public long Id_Image { get; set; }

        [CustomAttributeMapper("id_stock")]
        public long Id_Stock { get; set; }
    }
}
