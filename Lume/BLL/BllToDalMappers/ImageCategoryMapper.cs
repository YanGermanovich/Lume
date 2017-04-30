using DAL.DTO;
using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BllToDalMappers
{
    public static class ImageCategoryMapper
    {
        public static BllImageCategory ToBll(this DalImageCategory imgCategory)
        {
            if (imgCategory == null)
            {
                throw new ArgumentNullException(nameof(imgCategory));
            }
            return new BllImageCategory()
            {
                Id = imgCategory.Id,
                Name = imgCategory.Name
            };
        }

        public static DalImageCategory ToDal(this BllImageCategory imgCategory)
        {
            if (imgCategory == null)
            {
                throw new ArgumentNullException(nameof(imgCategory));
            }
            return new DalImageCategory()
            {
                Id = imgCategory.Id,
                Name = imgCategory.Name
            };
        }
    }
}
