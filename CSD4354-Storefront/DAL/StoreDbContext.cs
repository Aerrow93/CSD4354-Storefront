using CSD4354_Storefront.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CSD4354_Storefront.DAL
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext() : base("StoreDbContext")
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<ProductQty> ProductQty { get; set; }
        
        //Code to make auto-generated tables non-plural
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}