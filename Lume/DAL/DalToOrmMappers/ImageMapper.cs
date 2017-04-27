using DAL.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DalToOrmMappers
{
    public static class ImageMapper
    {
        public static image ToOrm(this DalImage img)
        {
            if (img == null)
                throw new ArgumentNullException(nameof(img));
            return new image()
            {
                id_image = img.Id,
                id_properties = img.Id_Properies,
                id_author = img.Id_Author,
                publication_date = img.PublicationDate,
                id_category_image = img.Id_ImageCategory,
                id_event = img.Id_Event,
                Image_src = img.Src,
                description_image = img.description,
                image_E = img.E,
                image_N = img.N,
                isConfirmed = img.IsConfirmed
            };
        }

        public static DalImage ToDal(this image img)
        {
            if (img == null)
                throw new ArgumentNullException(nameof(img));
            return new DalImage()
            {
                Id = img.id_image,
                Id_Properies = img.id_properties,
                Id_Author = img.id_author,
                PublicationDate = img.publication_date,
                Id_ImageCategory = img.id_category_image,
                Id_Event = img.id_event,
                Src = img.Image_src,
                description = img.description_image,
                E = img.image_E,
                N = img.image_N,
                IsConfirmed = img.isConfirmed
            };
        }
    }
}
