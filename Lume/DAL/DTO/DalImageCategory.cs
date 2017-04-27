using CustomExpressionVisitor;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class DalImageCategory : IEntity
    {
        [CustomAttributeMapper("id_category_image")]
        public long Id { get; set; }

        [CustomAttributeMapper("category_image1")]
        public string Name { get; set; }
    }
}
