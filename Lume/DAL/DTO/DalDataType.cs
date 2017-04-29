using CustomExpressionVisitor;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class DalDataType : IEntity
    {
        [CustomAttributeMapper("id_Type")]
        public long Id { get; set; }

        [CustomAttributeMapper("type_data")]
        public string Name { get; set; }
    }
}
