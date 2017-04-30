using CustomExpressionVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllStockPrize
    {
        [CustomAttributeMapper("Id")]
        public long Id { get; set; }

        [CustomAttributeMapper("Id_Stock")]
        public long Id_Stock { get; set; }

        [CustomAttributeMapper("Id_Prize")]
        public long Id_Prize { get; set; }
    }
}
