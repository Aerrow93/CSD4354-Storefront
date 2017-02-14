using CSD4354_Storefront.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CSD4354_Storefront.DAL
{
    public class StoreInitializer : DropCreateDatabaseIfModelChanges<StoreDbContext>
    {
        protected override void Seed(StoreDbContext context)
        {
            var products = new List<Product>
            {

            };
            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();
        }
    }
}