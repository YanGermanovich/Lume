using CustomExpressionVisitor;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class DalUserStock : IEntity
    {
        [CustomAttributeMapper("id_User_and_Stock")]
        public long Id { get; set; }

        [CustomAttributeMapper("id_stock")]
        public long Id_Stock { get; set; }

        [CustomAttributeMapper("id_user")]
        public long Id_User {get;set;}

        [CustomAttributeMapper("stock_progress")]
        public bool Progress { get; set; }
    }
}
