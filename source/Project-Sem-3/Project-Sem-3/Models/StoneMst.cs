//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project_Sem_3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StoneMst
    {
        public int ID { get; set; }
        public Nullable<int> Item_ID { get; set; }
        public int StoneQuality_ID { get; set; }
        public int StoneType_ID { get; set; }
        public decimal Carat { get; set; }
        public string Dimension { get; set; }
        public int Amount { get; set; }
    
        public virtual ItemMst ItemMst { get; set; }
        public virtual StoneQualityMst StoneQualityMst { get; set; }
        public virtual StoneTypeMst StoneTypeMst { get; set; }
    }
}
