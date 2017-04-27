using CustomExpressionVisitor;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class DalPrize : IEntity
    {
        [CustomAttributeMapper("id_prize")]
        public long Id { get; set; }

        [CustomAttributeMapper("id_type_prize")]
        public long Id_PrizeType { get; set; }

        [CustomAttributeMapper("prize_description")]
        public string Description { get; set; }

        [CustomAttributeMapper("prize_data")]
        public string Data { get; set; }
    }
}
