using DAL.DTO;
using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BllToDalMappers
{
    public static class ImageMapper
    {
        public static BllImage ToBll(this DalImage img)
        {
            if (img == null)
            {
                throw new ArgumentNullException(nameof(img));
            }
            return new BllImage()
            {
                Id = img.Id,
                Id_Properies = img.Id_Properies,
                Id_Author = img.Id_Author,
                PublicationDate = img.PublicationDate,
                Id_ImageCategory = img.Id_ImageCategory,
                Id_Event = img.Id_Event,
                Src = img.Src,
                description = img.description,
                E = img.E,
                N = img.N,
                IsConfirmed = img.IsConfirmed
            };
        }

        public static DalImage ToDal(this BllImage img)
        {
            if (img == null)
            {
                throw new ArgumentNullException(nameof(img));
            }
            return new DalImage()
            {
                Id = img.Id,
                Id_Properies = img.Id_Properies,
                Id_Author = img.Id_Author,
                PublicationDate = img.PublicationDate,
                Id_ImageCategory = img.Id_ImageCategory,
                Id_Event = img.Id_Event,
                Src = img.Src,
                description = img.description,
                E = img.E,
                N = img.N,
                IsConfirmed = img.IsConfirmed
            };
        }
    }
}
