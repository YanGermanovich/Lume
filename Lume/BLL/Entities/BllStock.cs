using CustomExpressionVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllStock
    {
        [CustomAttributeMapper("Id")]
        public long Id { get; set; }

        [CustomAttributeMapper("Name")]
        public string Name { get; set; }

        [CustomAttributeMapper("Id_Author")]
        public long Id_Author { get; set; }

        [CustomAttributeMapper("Id_StockType")]
        public long Id_StockType { get; set; }

        [CustomAttributeMapper("Description")]
        public string Description { get; set; }

        [CustomAttributeMapper("BeginingDate")]
        public DateTime BeginingDate { get; set; }

        [CustomAttributeMapper("FinishingDate")]
        public DateTime? FinishingDate { get; set; }
    }
}
