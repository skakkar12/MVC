//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Media.Growth.Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MediaChannelPlan
    {
        public int idMediaChannel { get; set; }
        public string Period { get; set; }
        public Nullable<int> Media { get; set; }
        public Nullable<int> SalesBase { get; set; }
        public Nullable<decimal> CarryOver { get; set; }
        public Nullable<decimal> UpliftMax { get; set; }
        public Nullable<int> SpendHalf { get; set; }
        public Nullable<int> SpendOpt { get; set; }
    
        public virtual ContactPoint ContactPoint { get; set; }
    }
}
