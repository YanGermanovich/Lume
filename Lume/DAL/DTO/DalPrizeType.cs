using CustomExpressionVisitor;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class DalPrizeType: IEntity
    {
        [CustomAttributeMapper("id_type")]
        public long Id { get; set; }

        [CustomAttributeMapper("prize_type")]
        public string Name { get; set; }
    }
}
