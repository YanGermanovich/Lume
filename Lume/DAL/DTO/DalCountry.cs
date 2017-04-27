using CustomExpressionVisitor;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class DalCountry : IEntity
    {
        [CustomAttributeMapper("id_country")]
        public long Id { get; set; }

        [CustomAttributeMapper("country_name")]
        public string Name { get; set; }
    }
}
