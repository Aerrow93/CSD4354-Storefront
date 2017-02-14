using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSD4354_Storefront.Models
{

    public class Product
    {
        public int Id { get; set; }
        public String ProductName { get; set; }
        public String Description { get; set; }
        public String Flavour1 { get; set; }
        public String Flavour2 { get; set; }
        public double PriceWholesale { get; set; }
        public double PricePackage { get; set; }
        public String PackageSize { get; set; }
        public int OnHandQuantity { get; set; }
        public String Location { get; set; }
        public String Bin { get; set; }
        public String Colour1 { get; set; }
        public String Colour2 { get; set; }
    }

    public class ProductDetailsViewModel
    {
        public Product Item { get; set; }
        public int Quantity { get; set; }

    }
}