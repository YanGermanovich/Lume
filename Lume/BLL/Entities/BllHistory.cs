using CustomExpressionVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllHistory 
    {
        [CustomAttributeMapper("Id")]
        public long Id { get; set; }

        [CustomAttributeMapper("Id_User")]
        public long Id_User { get; set; }

        [CustomAttributeMapper("Id_Image")]
        public long Id_Image { get; set; }

        [CustomAttributeMapper("N")]
        public double? N { get; set; }

        [CustomAttributeMapper("E")]
        public double? E { get; set;}

        [CustomAttributeMapper("ScaningDate")]
        public DateTime ScaningDate { get; set; }
    }
}
