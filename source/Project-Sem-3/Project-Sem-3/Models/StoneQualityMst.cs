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
    
    public partial class StoneQualityMst
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StoneQualityMst()
        {
            this.StoneMsts = new HashSet<StoneMst>();
        }
    
        public int ID { get; set; }
        public string Quality { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoneMst> StoneMsts { get; set; }
    }
}
