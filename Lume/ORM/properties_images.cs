//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ORM
{
    using System;
    using System.Collections.Generic;
    
    public partial class properties_images
    {
        public properties_images()
        {
            this.images = new HashSet<image>();
        }
    
        public long id_propertie { get; set; }
        public int width_image { get; set; }
        public int height_image { get; set; }
    
        public virtual ICollection<image> images { get; set; }
    }
}
