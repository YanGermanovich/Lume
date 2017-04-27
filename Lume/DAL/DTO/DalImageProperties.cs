using CustomExpressionVisitor;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class DalImageProperties : IEntity
    {
        [CustomAttributeMapper("id_propertie")]
        public long Id { get; set; }

        [CustomAttributeMapper("width_image")]
        public int Width { get; set; }

        [CustomAttributeMapper("height_image")]
        public int Height { get; set; }
    }
}
