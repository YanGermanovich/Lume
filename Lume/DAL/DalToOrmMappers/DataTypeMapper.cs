using DAL.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DalToOrmMappers
{
    public static class DataTypeMapper
    {
        public static type_of_data ToOrm(this DalDataType dataType)
        {
            if (dataType == null)
            {
                throw new ArgumentNullException(nameof(dataType));
            }
            return new type_of_data()
            {
                id_Type = dataType.Id,
                type_data = dataType.Name
            };
        }

        public static DalDataType ToDal(this type_of_data dataType)
        {
            if (dataType == null)
            {
                throw new ArgumentNullException(nameof(dataType));
            }
            return new DalDataType()
            {
                Id= dataType.id_Type,
                Name= dataType.type_data
            };
        }
    }
}
