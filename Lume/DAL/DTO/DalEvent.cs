using CustomExpressionVisitor;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class DalEvent : IEntity
    {
        [CustomAttributeMapper("id_event")]
        public long Id { get; set; }

        [CustomAttributeMapper("Source")]
        public string Source { get; set; }

        [CustomAttributeMapper("Type_id_Type")]
        public long Id_DataType { get; set; }
    }
}
