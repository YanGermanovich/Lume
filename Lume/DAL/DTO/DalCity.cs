using CustomExpressionVisitor;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class DalCity : IEntity
    {
        [CustomAttributeMapper("id_city")]
        public long Id { get; set; }

        [CustomAttributeMapper("city_name")]
        public string Name { get; set; }

        [CustomAttributeMapper("id_country")]
        public long Id_Country { get; set; }
    }
}
