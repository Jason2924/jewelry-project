using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Sem_3.Models
{
    public class Product
    {
        public ItemMst Item { get; set; }
        public Nullable<int> Discount { get; set; }
        public Nullable<decimal> Sale { get; set; }
    }
}