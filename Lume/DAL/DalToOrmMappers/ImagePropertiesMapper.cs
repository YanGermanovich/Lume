using DAL.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DalToOrmMappers
{
    public static class ImagePropertiesMapper
    {
        public static properties_images ToOrm(this DalImageProperties imgProp)
        {
            if (imgProp == null)
                throw new ArgumentNullException(nameof(imgProp));
            return new properties_images()
            {
                id_propertie = imgProp.Id,
                height_image = imgProp.Height,
                width_image = imgProp.Width
            };
        }

        public static DalImageProperties ToDal(this properties_images imgProp)
        {
            if (imgProp == null)
                throw new ArgumentNullException(nameof(imgProp));
            return new DalImageProperties()
            {
                Id = imgProp.id_propertie,
                Height = imgProp.height_image,
                Width= imgProp.width_image
            };
        }
    }
}
