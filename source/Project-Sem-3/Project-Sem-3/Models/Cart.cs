﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Sem_3.Models
{
    public class Cart
    {
        public ItemMst Item { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}