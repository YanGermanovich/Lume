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
    
    public partial class user_and_stock
    {
        public long id_User_and_Stock { get; set; }
        public long id_stock { get; set; }
        public long id_user { get; set; }
        public Nullable<bool> stock_progress { get; set; }
    
        public virtual stock stock { get; set; }
        public virtual user user { get; set; }
    }
}