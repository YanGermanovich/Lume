using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;
using DAL.DTO;

namespace DAL.DalToOrmMappers
{
    public static class ImageCategoryMapper
    {
        public static category_image ToOrm(this DalImageCategory imgCategory)
        {
            if (imgCategory == null)
                throw new ArgumentNullException(nameof(imgCategory));
            return new category_image()
            {
                id_category_image = imgCategory.Id,
                category_image1 = imgCategory.Name
            };
        }

        public static DalImageCategory ToDal(this category_image imgCategory)
        {
            if (imgCategory == null)
                throw new ArgumentNullException(nameof(imgCategory));
            return new DalImageCategory()
            {
                Id = imgCategory.id_category_image,
                Name = imgCategory.category_image1
            };
        }
    }
}
