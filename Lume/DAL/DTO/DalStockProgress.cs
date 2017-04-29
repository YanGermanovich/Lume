using CustomExpressionVisitor;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class DalStockProgress : IEntity
    {
        [CustomAttributeMapper("id_stock_progress")]
        public long Id { get; set; }

        [CustomAttributeMapper("id_user")]
        public long Id_User { get; set; }

        [CustomAttributeMapper("id_stock_and_Image")]
        public long Id_StockImage { get; set; }

        [CustomAttributeMapper("is_scanned")]
        public bool IsScanndes { get; set; }
    }
}
