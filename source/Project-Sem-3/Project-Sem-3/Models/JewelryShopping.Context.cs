﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class JewelryShoppingEntities : DbContext
    {
        public JewelryShoppingEntities()
            : base("name=JewelryShoppingEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AdminMst> AdminMsts { get; set; }
        public virtual DbSet<BrandMst> BrandMsts { get; set; }
        public virtual DbSet<CategoryMst> CategoryMsts { get; set; }
        public virtual DbSet<CertificateMst> CertificateMsts { get; set; }
        public virtual DbSet<DiscountMst> DiscountMsts { get; set; }
        public virtual DbSet<InquiryMst> InquiryMsts { get; set; }
        public virtual DbSet<ItemMst> ItemMsts { get; set; }
        public virtual DbSet<OrderDetailMst> OrderDetailMsts { get; set; }
        public virtual DbSet<OrderMst> OrderMsts { get; set; }
        public virtual DbSet<StoneMst> StoneMsts { get; set; }
        public virtual DbSet<StoneQualityMst> StoneQualityMsts { get; set; }
        public virtual DbSet<StoneTypeMst> StoneTypeMsts { get; set; }
        public virtual DbSet<UserMst> UserMsts { get; set; }
        public virtual DbSet<ItemView> ItemViews { get; set; }
    }
}