using CustomExpressionVisitor;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class DalHistory : IEntity
    {
        [CustomAttributeMapper("id_History")]
        public long Id { get; set; }

        [CustomAttributeMapper("id_user")]
        public long Id_User { get; set; }

        [CustomAttributeMapper("id_image")]
        public long Id_Image { get; set; }

        [CustomAttributeMapper("history_N")]
        public double? N { get; set; }

        [CustomAttributeMapper("history_E")]
        public double? E { get; set;}

        [CustomAttributeMapper("Date_scaning")]
        public DateTime ScaningDate { get; set; }
    }
}
