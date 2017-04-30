using CustomExpressionVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllStockProgress
    {
        [CustomAttributeMapper("Id")]
        public long Id { get; set; }

        [CustomAttributeMapper("Id_User")]
        public long Id_User { get; set; }

        [CustomAttributeMapper("Id_StockImage")]
        public long Id_StockImage { get; set; }

        [CustomAttributeMapper("IsScanndes")]
        public bool IsScannded { get; set; }
    }
}
