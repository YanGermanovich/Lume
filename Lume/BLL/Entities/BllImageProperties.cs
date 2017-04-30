using CustomExpressionVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllImageProperties
    {
        [CustomAttributeMapper("Id")]
        public long Id { get; set; }

        [CustomAttributeMapper("Width")]
        public int Width { get; set; }

        [CustomAttributeMapper("Height")]
        public int Height { get; set; }
    }
}
