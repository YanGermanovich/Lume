using CustomExpressionVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllAvatar
    {
        [CustomAttributeMapper("Id")]
        public long Id { get; set; }

        [CustomAttributeMapper("Src")]
        public string Src { get; set; }
    }
}
