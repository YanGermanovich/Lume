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
    
    public partial class stock_prize
    {
        public long id_stock_prize { get; set; }
        public long Stock_id_stock { get; set; }
        public long id_prize { get; set; }
    
        public virtual prize prize { get; set; }
        public virtual stock stock { get; set; }
    }
}
