using CustomExpressionVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllStockImage
    {
        [CustomAttributeMapper("Id")]
        public long Id { get; set; }

        [CustomAttributeMapper("Id_Image")]
        public long Id_Image { get; set; }

        [CustomAttributeMapper("Id_Stock")]
        public long Id_Stock { get; set; }
    }
}
