using BLL.Entities;
using Lume.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lume.Infrastructure.Mappers
{
    public static class ImageMapper
    {
        public static BllImage ToBll(this ImageViewModel image)
        {
            return new BllImage()
            {
                Id = image.Id,
                Id_Properies = image.Id_Properies,
                Id_Author = image.Id_Author,
                Id_ImageCategory = image.Id_ImageCategory,
                Id_Event = image.Id_Event,
                PublicationDate = image.PublicationDate,
                description = image.description,
                Src = image.Src,
                N = image.N,
                E = image.E,
                IsConfirmed = image.IsConfirmed
            };
        }
    }
}