using CustomExpressionVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllImage  
    {
        [CustomAttributeMapper("Id")]
        public long Id { get; set; }

        [CustomAttributeMapper("Id_Properies")]
        public long? Id_Properies { get; set; }

        [CustomAttributeMapper("Id_Author")]
        public long? Id_Author { get; set; }

        [CustomAttributeMapper("Id_ImageCategory")]
        public long? Id_ImageCategory { get; set; }

        [CustomAttributeMapper("Id_Event")]
        public long? Id_Event { get; set; }

        [CustomAttributeMapper("PublicationDate")]
        public DateTime? PublicationDate { get; set; }

        [CustomAttributeMapper("description")]
        public string description { get; set; }

        [CustomAttributeMapper("Src")]
        public string Src { get; set; }

        [CustomAttributeMapper("N")]
        public double? N { get; set; }

        [CustomAttributeMapper("E")]
        public double? E { get; set; }

        [CustomAttributeMapper("IsConfirmed")]
        public bool IsConfirmed { get; set; }
    }
}
