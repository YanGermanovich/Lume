using DAL.DTO;
using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BllToDalMappers
{
    public static class ImagePropertiesMapper
    {
        public static BllImageProperties ToBll(this DalImageProperties imgProp)
        {
            if (imgProp == null)
            {
                throw new ArgumentNullException(nameof(imgProp));
            }
            return new BllImageProperties()
            {
                Id = imgProp.Id,
                Height = imgProp.Height,
                Width = imgProp.Width
            };
        }

        public static DalImageProperties ToDal(this BllImageProperties imgProp)
        {
            if (imgProp == null)
            {
                throw new ArgumentNullException(nameof(imgProp));
            }
            return new DalImageProperties()
            {
                Id = imgProp.Id,
                Height = imgProp.Height,
                Width= imgProp.Width
            };
        }
    }
}
