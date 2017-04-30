using CustomExpressionVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllPrize 
    {
        [CustomAttributeMapper("Id")]
        public long Id { get; set; }

        [CustomAttributeMapper("Id_PrizeType")]
        public long Id_PrizeType { get; set; }

        [CustomAttributeMapper("Description")]
        public string Description { get; set; }

        [CustomAttributeMapper("Data")]
        public string Data { get; set; }
    }
}
