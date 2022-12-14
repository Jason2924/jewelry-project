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
    
    public partial class ItemMst
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ItemMst()
        {
            this.DiscountMsts = new HashSet<DiscountMst>();
            this.OrderDetailMsts = new HashSet<OrderDetailMst>();
            this.StoneMsts = new HashSet<StoneMst>();
        }
    
        public int ID { get; set; }
        public string StyleCode { get; set; }
        public int Brand_ID { get; set; }
        public int Category_ID { get; set; }
        public int Certificate_ID { get; set; }
        public string Name { get; set; }
        public Nullable<byte> NumberInSet { get; set; }
        public Nullable<int> Quantity { get; set; }
        public byte GoldKarat { get; set; }
        public decimal GoldWeight { get; set; }
        public Nullable<byte> Wastage { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> MRP { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    
        public virtual BrandMst BrandMst { get; set; }
        public virtual CategoryMst CategoryMst { get; set; }
        public virtual CertificateMst CertificateMst { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiscountMst> DiscountMsts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetailMst> OrderDetailMsts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoneMst> StoneMsts { get; set; }
    }
}
