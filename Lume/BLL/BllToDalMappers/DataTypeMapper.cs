using DAL.DTO;
using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BllToDalMappers
{
    public static class DataTypeMapper
    {
        public static BllDataType ToBll(this DalDataType dataType)
        {
            if (dataType == null)
            {
                throw new ArgumentNullException(nameof(dataType));
            }
            return new BllDataType()
            {
                Id = dataType.Id,
                Name = dataType.Name
            };
        }

        public static DalDataType ToDal(this BllDataType dataType)
        {
            if (dataType == null)
            {
                throw new ArgumentNullException(nameof(dataType));
            }
            return new DalDataType()
            {
                Id = dataType.Id,
                Name = dataType.Name
            };
        }
    }
}
