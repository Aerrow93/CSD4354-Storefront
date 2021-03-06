﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSD4354_Storefront.Models
{
    public enum CartStatus { OPEN, CHECKING_OUT, PAID, SHIPPED };

    public class Cart
    {
        public int Id { get; set; }
        public User Purchaser { get; set; }
        public List<ProductQty> Items { get; set; }
        public double Discount { get; set; }
        public int PaymentId { get; set; }
        public double TaxRate { get; set; }
        public String Tracking { get; set; }
        public DateTime Date { get; set; }
        public CartStatus Status { get; set; }
    }

    public class ProductQty
    {
        public int Id { get; set; }
        public Product Item { get; set; }
        public int Qty { get; set; }
    }
}