using CustomExpressionVisitor;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class DalImage : IEntity
    {
        [CustomAttributeMapper("id_image")]
        public long Id { get; set; }

        [CustomAttributeMapper("id_properties")]
        public long? Id_Properies { get; set; }

        [CustomAttributeMapper("id_author")]
        public long? Id_Author { get; set; }

        [CustomAttributeMapper("id_category_image")]
        public long? Id_ImageCategory { get; set; }

        [CustomAttributeMapper("id_event")]
        public long? Id_Event { get; set; }

        [CustomAttributeMapper("id_propertie")]
        public DateTime? PublicationDate { get; set; }

        [CustomAttributeMapper("description_image")]
        public string description { get; set; }

        [CustomAttributeMapper("Image_src")]
        public string Src { get; set; }

        [CustomAttributeMapper("image_N")]
        public double? N { get; set; }

        [CustomAttributeMapper("image_E")]
        public double? E { get; set; }

        [CustomAttributeMapper("isConfirmed")]
        public bool IsConfirmed { get; set; }
    }
}
