using CustomExpressionVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllEvent
    {
        [CustomAttributeMapper("Id")]
        public long Id { get; set; }

        [CustomAttributeMapper("Source")]
        public string Source { get; set; }

        [CustomAttributeMapper("Id_DataType")]
        public long Id_DataType { get; set; }
    }
}
